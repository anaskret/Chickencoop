<template>
  <v-dialog v-model="$store.state.showAutoLogoutModal" max-width="790">
    <v-card>
      <v-card-title class="justify-center">
        Session Expired
      </v-card-title>
      <v-card-subtitle>
        You have left this browser idle for 10 minutes.
      </v-card-subtitle>
    </v-card>
  </v-dialog>
</template>
