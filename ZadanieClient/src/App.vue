<template>
  <v-app id="app">
    <Navigation/>
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
import Navigation from "./components/Navigation";

export default {
  name: "App",

  components: {
    Navigation,
  },
};
</script>
