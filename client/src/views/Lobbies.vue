<template>
    <v-app>
        <v-menu
        v-model="showMenu"
        absolute
        offset-y
        style="max-width: 600px"
        :close-on-content-click=false
        >
            <template v-slot:activator="{ on, attrs }">
                <v-btn
                elevation="2"
                outlined
                absolute
                permanent
                right
                v-bind="attrs"
                v-on="on"
                >New player</v-btn>
            </template>

            <new-player/>
        </v-menu>
    
        <v-card
            max-width="600"
            class="mx-auto"
        >
            <v-toolbar
            color="light-blue"
            dark
            >
            <v-toolbar-title>Lobbies</v-toolbar-title>

            <v-spacer></v-spacer>

            </v-toolbar>

            <v-list
            subheader
            two-line
            >

            <v-list-item>

                <v-list-item-content>
                <v-list-item-title></v-list-item-title>

                <v-list-item-subtitle></v-list-item-subtitle>
                </v-list-item-content>

                <v-list-item-action>
                    <v-btn icon>
                        <v-icon color="grey lighten-1">mdi-information</v-icon>
                    </v-btn>
                </v-list-item-action>
            </v-list-item>
            </v-list>

            <v-btn
            @click="$router.push('/lobbies/newlobby')"
            elevation="2"
            outlined
            >Create new lobby</v-btn>
        </v-card>
    </v-app>
</template>

<script>
export default {
    data: () => ({
        showMenu: false,
    }),
    
}
</script>

<style>
</style>