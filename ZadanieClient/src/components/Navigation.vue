<template>
  <v-card height="400" width="256">
    <v-list-item>
      <v-list-item-content>
        <v-list-item-title class="text-h6"> TestApp </v-list-item-title>
        <v-list-item-subtitle> Employee management app </v-list-item-subtitle>
      </v-list-item-content>
    </v-list-item>

    <v-divider></v-divider>

    <v-list dense nav>
      <v-list-item v-for="item in items" :key="item.title"
        :to="item.route"
       link>
        <v-list-item-icon>
          <v-icon>{{ item.icon }}</v-icon>
        </v-list-item-icon>

        <v-list-item-content>
          <v-list-item-title>{{ item.title }}</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>
    
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      items: [
        { title: "Employees", route: "/employees", icon: "mdi-account-group-outline" },
        { title: "Past employees", route: "/past-employees", icon: "mdi-account-group-outline" },
        { title: "Positions", route: "/positions", icon: "mdi-view-dashboard" },
        { title: "About", route: "/about", icon: "mdi-help-box" },
      ],
      right: null,
    };
  },
};
</script>

<style lang="sass" scoped>
</style>