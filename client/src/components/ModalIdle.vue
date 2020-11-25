<template>
  <v-dialog v-model="$store.state.showAutoLogoutModal" max-width="790">
    <v-card>
      <v-card-title>
        Session Expired
      </v-card-title>
      <v-card-text>
        You have left this browser idle for 10 minutes.
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script></script>
