<template>
  <v-card full-width class="mx-auto">
    <v-card-title>
      Player ranking
      <v-spacer></v-spacer>
      <v-text-field
        v-model="search"
        append-icon="mdi-magnify"
        label="Search"
        single-line
        hide-details
      ></v-text-field>
    </v-card-title>

    <v-data-table
      :headers="headers"
      :items="players"
      :items-per-page="10"
      class="elevation-1"
      :search="search"
    >
      <template v-slot:[`item.actions`]="{ item }">
        <v-icon
          small
          class="mr-2"
          color="green"
          v-if="item.isOnline"
          @click="goToProfile(item.id)"
        >
          mdi-account
        </v-icon>
        <v-icon
          small
          class="mr-2"
          color="red"
          v-else
          @click="goToProfile(item.id)"
        >
          mdi-account
        </v-icon>
      </template>
    </v-data-table>
  </v-card>
</template>

<script>
import PersonalLeaderboardDataService from "../services/PersonalLeaderboardDataService";

export default {
  data() {
    return {
      search: "",
      headers: [
        { text: "Player", value: "nickname" },
        { text: "Wins", value: "wins" },
        {
          text: "Profile",
          value: "actions",
          sortable: false,
          align: "center",
          width: "5%"
        }
      ],
      players: []
    };
  },
  created() {
    this.getRanking();
  },
  updated() {
    this.getRanking();
  },
  methods: {
    getRanking() {
      PersonalLeaderboardDataService.getRanking().then(res => {
        this.players = res.data;
      });
    },
    goToProfile(id) {
      this.$router.push({ name: "Profile", params: { id: id } });
    }
  }
};
</script>
