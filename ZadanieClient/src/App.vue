<template>
  <v-app>
    <v-container>
      <v-row>
        <v-col cols="12"  sm="3" md="3">
          <v-navigation-drawer permanent left>
            <Navigation />
          </v-navigation-drawer>
        </v-col>
        <v-col cols="12" sm="6" md="6">
          <router-view />
        </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>

<script>
import Navigation from "./components/Navigation";

export default {
  name: "App",

  components: {
    Navigation,
  },

  data: () => ({
    //
  }),
};
</script>


<style lang="scss">
</style>
