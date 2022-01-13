<template>
  <v-data-table
    :headers="headers"
    :items="employees"
    :search="search"
    sort-by="fullname"
    class="py-8 px-4"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title class="primary--text text-h4"
          >Past Employees</v-toolbar-title
        >
        <v-spacer></v-spacer>
        <v-text-field
          class="mb-3 pr-2 mt-6"
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
        <div>
          <v-snackbar v-model="snackbar" :timeout="timeout" :multi-line="true">
            {{ snackbar_text }}

            <template v-slot:action="{ attrs }">
              <v-btn
                color="primary"
                text
                v-bind="attrs"
                @click="snackbar = false"
              >
                Close
              </v-btn>
            </template>
          </v-snackbar>
        </div>
        <v-progress-linear
          :active="loading"
          :indeterminate="loading"
          absolute
          bottom
          color="primary"
        ></v-progress-linear>
        <v-dialog v-model="dialog" max-width="800px">
          <v-card>
            <v-card-title>
              <span class="text-h5 primary--text">View Employee</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      disabled
                      v-model="editedItem.name"
                      label="Name(s)"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      disabled
                      v-model="editedItem.surname"
                      label="Surname(s)"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="12" md="12">
                    <v-text-field
                      disabled
                      v-model="editedItem.address"
                      label="Address"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      disabled
                      v-model="editedItem.dateOfBirth"
                      label="Date of Birth"
                      prepend-icon="mdi-calendar"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      disabled
                      v-model="editedItem.startDate"
                      label="Start Date"
                      prepend-icon="mdi-calendar"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      disabled
                      v-model="editedItem.salary"
                      label="Salary"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      disabled
                      v-model="editedItem.positionName"
                      label="Position"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      disabled
                      v-model="editedItem.endDate"
                      label="End Date"
                      prepend-icon="mdi-calendar"
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" @click="close"> Cancel </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h7 text-center"
              >Are you sure you want to permanently delete this
              Employee?</v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="closeDelete">Cancel</v-btn>
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
        @click="viewItem(item)"
      >
        <v-icon>mdi-eye</v-icon>
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
      <div v-if="!loading" class="text-center">
        <h4>No employee data found</h4>
        <v-btn color="primary" @click="initialize"> Reload data </v-btn>
      </div>
      <div v-if="loading" class="text-center">
        <h4>Loading past employees...</h4>
      </div>
    </template>
  </v-data-table>
</template>

<script>
import { Employee } from "../classes/employee";
import axios from "axios";
import {
  PastEmployeeURL,
  PositionURL,
  handleErrorMsg,
} from "../classes/common";

var _employee = new Employee();

export default {
  data: () => ({
    loading: false,
    snackbar: false,
    snackbar_text: "",
    timeout: 3000,
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
        value: "fullname",
      },
      { text: "Position", sortable: true, value: "positionName" },
      { text: "End Date", sortable: true, value: "endDate" },
      { text: "", value: "actions", sortable: false },
    ],
    employees: [],
    editedIndex: -1,
    editedItem: {
      employeeId: "",
      fullname: "",
      name: "",
      surname: "",
      address: "",
      dateOfBirth: "",
      salary: 0.0,
      startDate: new Date().toISOString().substr(0, 10),
      positionId: 0,
      positionName: "",
      endDate: new Date().toISOString().substr(0, 10),
    },
    defaultItem: {
      employeeId: "",
      fullname: "",
      name: "",
      surname: "",
      address: "",
      dateOfBirth: "",
      salary: 0.0,
      startDate: new Date().toISOString().substr(0, 10),
      positionId: 0,
      positionName: "",
      endDate: new Date().toISOString().substr(0, 10),
    },
  }),

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
    initialize() {
      this.loading = true;
      const posRequest = axios.get(PositionURL);
      const emplRequest = axios.get(PastEmployeeURL);

      axios
        .all([posRequest, emplRequest])
        .then(
          axios.spread((...responses) => {
            const positions = responses[0].data;
            const tmpEmployees = responses[1].data;

            tmpEmployees.forEach((el) => {
              this.employees.push(_employee.fillMissingData(el, positions));
            });
            this.loading = false;
          })
        )
        .catch((errors) => {
          this.showErrorMsg(errors);
          this.loading = false;
        });
    },

    viewItem(empl) {
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
      this.removeEmployee(this.editedItem.employeeId);
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

    removeEmployee(employeeId) {
      const removeEmployeePath = PastEmployeeURL + "/" + employeeId;
      axios
        .delete(removeEmployeePath)
        .then(() => {
          console.log(
            `Past Employee with id ${employeeId} was permanently removed`
          );
        })
        .catch((error) => {
          this.showErrorMsg(error);
        });
    },

    showErrorMsg(error) {
      console.error(error);

      let errMsg = handleErrorMsg(error);

      this.snackbar_text = errMsg;
      this.snackbar = true;
    },
  },
};
</script>