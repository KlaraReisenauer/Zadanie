<template>
  <v-data-table
    :headers="headers"
    :items="positions"
    :search="search"
    sort-by="name"
    class="elevation-1 py-8 px-4"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title class="primary--text text-h4"
          >Positions</v-toolbar-title
        >
        <v-spacer></v-spacer>
        <v-text-field
          class="mb-3 mt-6"
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
        <v-dialog v-model="dialog" max-width="500px" min-height="120px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              color="primary"
              dark
              class="mb-2 mt-4 ml-5"
              v-bind="attrs"
              v-on="on"
            >
              New Position
            </v-btn>
          </template>
          <v-form ref="form" v-model="valid" class="formclass pa-4">
            <v-container>
              <v-row>
                <span class="primary--text text-h5">{{ formTitle }}</span>
              </v-row>
              <v-row>
                <v-col cols="12" mr="2" md="10">
                  <v-text-field
                    v-model="editedItem.name"
                    label="Position name"
                    :rules="positionNameRules"
                    :counter="150"
                    required
                  ></v-text-field> </v-col
              ></v-row>
              <v-spacer></v-spacer>
              <v-row class="justify-end">
                <v-btn color="primary" text @click="close"> Cancel </v-btn>
                <v-btn :disabled="!valid" color="primary" @click="save">
                  Save
                </v-btn>
              </v-row>
            </v-container>
          </v-form>
        </v-dialog>
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h6 text-center"
              >Are you sure you want to delete this position?</v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="closeDelete">Cancel</v-btn>
              <v-btn color="primary" class="mb-2" @click="deleteItemConfirm"
                >OK</v-btn
              >
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:[`item.actions`]="{ item }">
      <v-btn
        class="mx-2"
        fab
        dark
        small
        color="primary"
        @click="editItem(item)"
      >
        <v-icon>mdi-pencil</v-icon>
      </v-btn>
      <v-btn
        class="mx-2"
        fab
        dark
        small
        color="error"
        @click="deleteItem(item)"
      >
        <v-icon>mdi-delete</v-icon>
      </v-btn>
    </template>
    <template v-slot:no-data>
      <v-btn color="primary" @click="initialize"> Reset </v-btn>
    </template>
  </v-data-table>
</template>


<script>
// import { Position } from "../classes/position";
import axios from "axios";
import { PositionURL } from "../classes/common";

export default {
  data: () => ({
    valid: false,
    positionNameRules: [
      (v) => !!v || "Position name is required",
      (v) =>
        (v && v.length > 1 && v.length <= 150) ||
        "Position name must have at least 2 characters, but no more than 150",
    ],
    search: "",
    dialog: false,
    dialogDelete: false,
    headers: [
      {
        text: "Position name",
        align: "start",
        sortable: true,
        value: "name",
      },
      { text: "", value: "actions", sortable: false },
    ],
    positions: [],
    editedIndex: -1,
    editedItem: {
      positionId: 0,
      name: "",
    },
    defaultItem: {
      positionId: 0,
      name: "",
    },
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "New Position" : "Edit Position";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },

  created() {
    this.initialize();
  },

  methods: {
    async initialize() {
      axios.get(PositionURL).then((result) => {
        console.log(`Positions were successfully retrieved`);
        this.positions = result.data;
      });

      // let _position = new Position();
      // _position.loadPositions().then((result) => {
      //   this.positions = result;
      // });
    },

    editItem(item) {
      this.editedIndex = this.positions.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      this.editedIndex = this.positions.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.removePosition(this.editedItem.positionId);
      this.positions.splice(this.editedIndex, 1);
      this.closeDelete();
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    save() {
      if (this.editedIndex > -1) {
        if (this.editedItem.name !== this.positions[this.editedIndex].name) {
          this.editExistingPosition(this.editedItem);
          Object.assign(this.positions[this.editedIndex], this.editedItem);
        }
      } else {
        this.addNewPosition(this.editedItem.name);
      }
      this.close();
    },

    editExistingPosition(position) {
      axios.post(PositionURL, position).then(() => {
        console.log(
          `Position ${JSON.stringify(position)} was successfully updated`
        );
      });
    },

    addNewPosition(positionName) {
      axios.post(PositionURL, { name: positionName }).then((result) => {
        console.log(
          `New position with id ${result.data} was successfully added`
        );
        this.positions.push({
          positionId: result.data,
          name: positionName,
        });
      });
    },

    removePosition(positionId) {
      const removePositionPath = PositionURL + "/" + positionId;
      axios.delete(removePositionPath).then(() => {
        console.log(`Position with id ${positionId} was successfully removed`);
      });
    },
  },
};
</script>

<style>
.formclass {
  background-color: white;
}
</style>