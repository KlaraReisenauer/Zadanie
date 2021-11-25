<template>
  <v-data-table
    :headers="headers"
    :items="employees"
    :search="search"
    sort-by="fullName"
    class="elevation-1"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>Employees</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-text-field
        v-model="search"
        append-icon="mdi-magnify"
        label="Search"
        single-line
        hide-details
      ></v-text-field>
        <v-dialog v-model="dialog" max-width="800px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
              New Employee
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ formTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      v-model="editedItem.name"
                      label="Name(s)"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      v-model="editedItem.surname"
                      label="Surname(s)"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="12" md="12">
                    <v-text-field
                      v-model="editedItem.address"
                      label="Address"
                    ></v-text-field>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="6" md="6">
                    <v-menu
                      v-model="birthDatePickerVisible"
                      :close-on-content-click="false"
                      :nudge-right="40"
                      transition="scale-transition"
                      offset-y
                      min-width="auto"
                    >
                      <template v-slot:activator="{ on, attrs }">
                        <v-text-field
                          v-model="editedItem.dateOfBirth"
                          label="Date of Birth"
                          prepend-icon="mdi-calendar"
                          readonly
                          v-bind="attrs"
                          v-on="on"
                        ></v-text-field>
                      </template>
                      <v-date-picker
                        v-model="editedItem.dateOfBirth"
                        @input="birthDatePickerVisible = false"
                        :max="today"
                      ></v-date-picker>
                    </v-menu>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-menu
                      v-model="startDatePickerVisible"
                      :close-on-content-click="false"
                      :nudge-right="40"
                      transition="scale-transition"
                      offset-y
                      min-width="auto"
                    >
                      <template v-slot:activator="{ on, attrs }">
                        <v-text-field
                          v-model="editedItem.startDate"
                          label="Start Date"
                          prepend-icon="mdi-calendar"
                          readonly
                          v-bind="attrs"
                          v-on="on"
                        ></v-text-field>
                      </template>
                      <v-date-picker
                        v-model="editedItem.startDate"
                        @input="startDatePickerVisible = false"
                        :min="today"
                      ></v-date-picker>
                    </v-menu>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      v-model="editedItem.salary"
                      label="Salary"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-select
                      solo
                      v-model="editedItem.position"
                      label="Position"
                      :items="positions"
                      item-text="name"
                    ></v-select>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="close"> Cancel </v-btn>
              <v-btn color="primary" @click="save"> Save </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h7"
              >Are you sure you want to delete this Employee?</v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeDelete"
                >Cancel</v-btn
              >
              <v-btn color="primary" @click="deleteItemConfirm">OK</v-btn>
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
export default {
  data: () => ({
    search: "",
    today: new Date().toISOString().substr(0, 10),
    dialog: false,
    dialogDelete: false,
    birthDatePickerVisible: false,
    startDatePickerVisible: false,
    headers: [
      {
        text: "Name",
        align: "start",
        sortable: true,
        value: "fullName",
      },
      { text: "Position", sortable: true, value: "position" },
      { text: "", value: "actions", sortable: false },
    ],
    employees: [],
    positions: [],
    editedIndex: -1,
    editedItem: {
      fullName: "",
      name: "",
      surname: "",
      address: "",
      dateOfBirth: "",
      salary: 0.0,
      startDate: new Date().toISOString().substr(0, 10),
      position: "",
    },
    defaultItem: {
      fullName: "",
      name: "",
      surname: "",
      address: "",
      dateOfBirth: "",
      salary: 0.0,
      startDate: new Date().toISOString().substr(0, 10),
      position: "",
    },
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "New Employee" : "Edit Employee";
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
    this.initPositions();
  },

  methods: {
    initialize() {
      this.employees = [
        {
          id: "8F7DE5AC-769A-44EF-B9F6-1C3AAA0A219B",
          fullName: "Sirius Black",
          name: "Sirius",
          surname: "Black",
          address: "",
          dateOfBirth: "",
          salary: 0,
          startDate: "",
          position: "Support",
        },
        {
          id: "8C2164E5-5670-4733-B888-2D8F44F6F704",
          fullName: "Lily Potter",
          name: "Lily",
          surname: "Potter",
          address: "",
          dateOfBirth: "",
          salary: 0,
          startDate: "",
          position: "Programator",
        },
        {
          id: "BBF73A4D-BF6E-4860-BDBE-66ADDD697012",
          fullName: "Harry Potter",
          name: "Harry",
          surname: "Potter",
          address: "4 Privet Drive, Surrey",
          dateOfBirth: "1980-07-31",
          salary: 2500.0,
          startDate: new Date("2021-11-19 12:29:59.490")
            .toISOString()
            .substr(0, 10),
          position: "Ine",
        },
        {
          id: "B3A11DB7-7E23-4775-9EBA-B5C98DF78401",
          fullName: "Hermione Granger",
          name: "Hermione",
          surname: "Granger",
          address: "",
          dateOfBirth: "",
          salary: 0,
          startDate: "",
          position: "Analytik",
        },
        {
          id: "633F0C41-BC3A-4674-A94F-E24531724EDF",
          fullName: "Ronald Weasley",
          name: "Ronald",
          surname: "Weasley",
          address: "",
          dateOfBirth: "",
          salary: 0,
          startDate: "",
          position: "Tester",
        },
        {
          id: "F58C8E6D-2AA7-4FEC-A647-EA336C70BC5F",
          fullName: "James Potter",
          name: "James",
          surname: "Potter",
          address: "",
          dateOfBirth: "",
          salary: 0,
          startDate: "",
          position: "Programator",
        },
      ];
    },

    initPositions() {
      this.positions = [
        {
          id: 1,
          name: "Ine",
        },
        {
          id: 2,
          name: "Tester",
        },
        {
          id: 3,
          name: "Programator",
        },
        {
          id: 4,
          name: "Support",
        },
        {
          id: 5,
          name: "Analytik",
        },
        {
          id: 6,
          name: "Obchodnik",
        }
      ];
    },

    editItem(empl) {
      this.editedIndex = this.employees.indexOf(empl);
      this.editedItem = Object.assign({}, empl);
      this.dialog = true;
    },

    deleteItem(empl) {
      this.editedIndex = this.employees.indexOf(empl);
      this.editedItem = Object.assign({}, empl);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.employees.splice(this.editedIndex, 1);
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
        Object.assign(this.employees[this.editedIndex], this.editedItem);
      } else {
        this.employees.push(this.editedItem);
      }
      this.close();
    },
  },
};
</script>

<style lang="sass" scoped>
</style>