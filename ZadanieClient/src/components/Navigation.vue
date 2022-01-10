<template>
  <v-navigation-drawer v-model="drawer" app>
    <v-sheet color="grey lighten-4" class="pa-4">
      <v-list-item>
        <v-list-item-content>
          <v-list-item-title class="text-h6 primary--text">
            TestApp
          </v-list-item-title>
          <br />
          <v-list-item-subtitle class="pt-1">
            Employee management app
          </v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-sheet>

    <v-divider></v-divider>

    <v-list nav>
      <v-list-item
        v-for="item in navigation_links"
        :key="item.title"
        :to="item.route"
        link
      >
        <v-list-item-icon>
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-item-icon>

        <v-list-item-content class="text-md-left">
          <v-list-item-title>{{ item.title }}</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
export default {
  data() {
    return {
      drawer: null,
      navigation_links: [
        {
          title: "Employees",
          route: "/employees",
          icon: "mdi-account-group-outline",
        },
        {
          title: "Past employees",
          route: "/past-employees",
          icon: "mdi-account-group-outline",
        },
        { title: "Positions", route: "/positions", icon: "mdi-view-dashboard" },
        { title: "About", route: "/about", icon: "mdi-help-box" },
      ],
    };
  },
};
</script>

<style lang="sass" scoped>
</style>