<template>
  <v-data-table
    :headers="headers"
    :items="employees"
    :search="search"
    sort-by="fullname"
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
                      
                    ><!-- :disabled="editingModeOpen" -->
                      <template v-slot:activator="{ on, attrs }">
                        <v-text-field
                          v-model="editedItem.dateOfBirth"
                          label="Date of Birth"
                          prepend-icon="mdi-calendar"
                          readonly
                          v-bind="attrs"
                          v-on="on"
                          :disabled="editingModeOpen"
                          required
                        ></v-text-field>
                      </template>
                      <v-date-picker
                          :disabled="editingModeOpen"
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
                      v-model.number="editedItem.salary"
                      type="number"
                      label="Salary"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-select
                      solo
                      v-model="editedItem.positionName"
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
import { Employee } from "../classes/employee";
var _employees = new Employee();

export default {
  data: () => ({
    search: "",
    today: new Date().toISOString().substr(0, 10),
    dialog: false,
    editingModeOpen: false,
    dialogDelete: false,
    birthDatePickerVisible: false,
    startDatePickerVisible: false,
    headers: [
      {
        text: "Name",
        align: "start",
        sortable: true,
        value: "fullname",
      },
      { text: "Position", sortable: true, value: "positionName" },
      { text: "", value: "actions", sortable: false },
    ],
    employees: [],
    positions: [],
    editedIndex: -1,
    editedItem: {
      id: "",
      fullname: "",
      name: "",
      surname: "",
      address: "",
      dateOfBirth: "",
      salary: 0.0,
      startDate: new Date().toISOString().substr(0, 10),
      positionId: 0,
      positionName: "",
    },
    defaultItem: {
      id: "",
      fullname: "",
      name: "",
      surname: "",
      address: "",
      dateOfBirth: "",
      salary: 0.0,
      startDate: new Date().toISOString().substr(0, 10),
      positionId: 0,
      positionName: "",
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
      this.employees = _employees.loadEmployees();
    },

    initPositions() {
      this.positions = _employees.loadPositions();
    },

    editItem(empl) {
      this.editedIndex = this.employees.indexOf(empl);
      this.editingModeOpen = true;
      this.editedItem = Object.assign({}, empl);
      this.dialog = true;
    },

    deleteItem(empl) {
      this.editedIndex = this.employees.indexOf(empl);
      this.editedItem = Object.assign({}, empl);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      _employees.deleteEmployee(this.editedItem.id);
      this.employees.splice(this.editedIndex, 1);
      this.closeDelete();
    },

    close() {
      this.editingModeOpen = false;
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
        if (
          _employees.isChanged(
            this.employees[this.editedIndex],
            this.editedItem
          )
        ) {
          Object.assign(
            this.employees[this.editedIndex],
            _employees.editEmployee(this.editedItem)
          );
        }
      } else {
        this.employees.push(_employees.addNewEmployee(this.editedItem));
      }
      this.close();
    },
  },
};
</script>

<style lang="sass" scoped>
</style>