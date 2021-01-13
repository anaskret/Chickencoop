<template>
  <v-card full-width class="mx-auto">
    <v-img height="100%" :src="$store.state.background"></v-img>
    <v-col>
      <v-avatar size="100" style="position:absolute; top: 130px">
        <v-img :src="$store.state.avatar"></v-img>
      </v-avatar>
      <v-row>
        <h1 class="nickname">{{ this.nickname }}</h1>
        <v-btn
          class="edit-profile"
          elevation="2"
          outlined
          @click="startEditing = true"
        >
          Edit Profile
          <v-icon>
            mdi-pencil-outline
          </v-icon>
        </v-btn>
      </v-row>
    </v-col>

    <v-data-table
      :headers="headers"
      :items="records"
      :items-per-page="10"
      class="elevation-1"
    >
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon small class="mr-2" @click="goToProfile(item.opponentId)">
          mdi-account
        </v-icon>
      </template>
    </v-data-table>

    <v-dialog v-model="startEditing" max-width="780">
      <v-card>
        <v-toolbar color="cyan" dark flat>
          <v-toolbar-title>Photo Manager</v-toolbar-title>

          <v-file-input
            accept="image/*"
            label="File input"
            prepend-icon="mdi-camera"
            @change="editProfile($event)"
            style="position: relative;
            left:30px;
            top:10px"
          />

          <v-btn
            outlined
            style="position: absolute;
            right: 10px;"
          >
            Upload image
          </v-btn>

          <v-spacer></v-spacer>

          <template v-slot:extension>
            <v-tabs v-model="tab" align-with-title>
              <v-tabs-slider color="yellow"></v-tabs-slider>

              <v-tab v-for="item in items" :key="item">
                {{ item }}
              </v-tab>
            </v-tabs>
          </template>
        </v-toolbar>

        <v-tabs-items v-model="tab">
          <v-tab-item v-for="item in items" :key="item">
            <v-card flat>
              <v-list-item v-for="(image, index) in images" :key="index">
                <v-list-item-avatar>
                  <v-img
                    :src="image.url"
                    lazy-src="https://picsum.photos/id/11/10/6"
                    max-height="40"
                    max-width="40"
                  >
                  </v-img>
                </v-list-item-avatar>

                <v-list-item-content>
                  <v-list-item-title v-text="image.name"></v-list-item-title>
                </v-list-item-content>

                <v-list-item-action>
                  <v-row>
                    <v-btn
                      color="green"
                      dark
                      style="right: 8px"
                      @click="imageSelected(image.url)"
                    >
                      Select
                    </v-btn>
                    <v-btn color="error" @click="removeImage(image.key)">
                      Delete
                    </v-btn>
                  </v-row>
                </v-list-item-action>
              </v-list-item>
            </v-card>
          </v-tab-item>
        </v-tabs-items>
      </v-card>
    </v-dialog>
  </v-card>
</template>

<script>
import PersonalLeaderboardDataService from "../services/PersonalLeaderboardDataService";
import PlayerDataService from "../services/PlayerDataService";
import S3ImageService from "../services/S3ImageService";

export default {
  data() {
    return {
      headers: [
        { text: "Opponent", value: "opponentNickname" },
        { text: "Result", value: "result" },
        { text: "Game", value: "gameName" },
        { text: "Game Time", value: "gameTime" },
        { text: "Game Date", value: "gameDate" },
        {
          text: "Actions",
          value: "actions",
          sortable: false,
          align: "center",
          width: "5%"
        }
      ],
      records: [],
      nickname: "nobody",
      startEditing: false,
      tab: 0,
      items: ["avatar", "background"],
      images: [
        {
          name: "photo"
        },
        {
          name: "photo2"
        }
      ]
    };
  },
  created() {
    this.getAllByPlayerId();
    this.getPlayer();
  },
  updated() {
    this.getAllByPlayerId();
    this.getPlayer();
  },
  methods: {
    getAllByPlayerId() {
      PersonalLeaderboardDataService.getAllByPlayerId(
        this.$route.params.id
      ).then(res => {
        this.records = res.data;
      });
    },
    getPlayer() {
      PlayerDataService.getById(this.$route.params.id).then(res => {
        this.nickname = res.data.nickname;
      });
    },
    goToProfile(id) {
      this.$router.push({ name: "Profile", params: { id: id } });
      window.location.reload();
    },
    editProfile(event) {
      const name = this.tab == 0 ? "avatar" : "background";
      S3ImageService.uploadImage(event, name);
    },
    imageSelected(image){
      PlayerDataService.getById(this.$route.params.id).then(res => {
        let data = {
          nickname: res.data.nickname,
          avatarUrl: res.data.avatarUrl,
          backgroundUrl: res.data.backgroundUrl,
          isOnline: res.data.isOnline
        };
        if (this.tab == 0) {
          data.avatarUrl = image;
          this.$store.state.avatar = image;
        } else if (this.tab == 1) {
          data.backgroundUrl = image;
          this.$store.state.backgroundUrl = image;
        }
        PlayerDataService.update(this.$route.params.id, data);
      });
      this.startEditing = false;
    },
    async listImages() {
      this.images = [];
      const name = this.tab == 0 ? "avatar" : "background";
      const res = await S3ImageService.listImages(name);
      // debugger;
      console.log(res);
      const promises = res.map(e => S3ImageService.getImgByKey(e.key));
      const results = Promise.all(promises)
      results.then(data=>{
        data.forEach((e,index)=>{
          this.images.push({url:e,...res[index]})
        })
      });
      // res.forEach(e=>{
      //   const img = await S3ImageService.getImgByKey(e.key);
      //   this.images.push({name:'xd',url:img})
      // })
      //this.images = res.map(image => ({ name: image.name }));
    },
    removeImage(key) {
      S3ImageService.removeImage(key).then(() => {
        this.images = this.images.filter(i => i.key !== key)
      });
    }
  },
  watch: {
    startEditing(val) {
      val && this.listImages();
    },
    tab() {
      this.listImages();
    }
  }
};
</script>

<style>
.nickname {
  position: relative;
  left: 10px;
}
.edit-profile {
  position: absolute;
  right: 10px;
  padding: 5px;
}
</style>
