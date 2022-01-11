<template>
  <v-data-table
    :headers="headers"
    :items="employees"
    :search="search"
    sort-by="fullname"
    class="elevation-1 py-8 px-4"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title class="primary--text text-h4"
          >Employees</v-toolbar-title
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
        <v-dialog v-model="dialog" max-width="800px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              color="primary"
              dark
              class="mb-2 mt-4 ml-5"
              @click="newEmployee"
              v-bind="attrs"
              v-on="on"
            >
              New Employee
            </v-btn>
          </template>
          <v-form ref="form" v-model="valid" class="formclass pa-4">
            <v-container>
              <v-row class="pa-8">
                <span class="primary--text text-h5">{{ formTitle }}</span>
              </v-row>

              <v-container class="pa-8">
                <v-row>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      v-model="editedItem.name"
                      label="Name(s)"
                      :rules="requiredFieldRules"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-text-field
                      v-model="editedItem.surname"
                      label="Surname(s)"
                      :rules="requiredFieldRules"
                      required
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
                          :rules="requiredFieldRules"
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
                          :rules="requiredFieldRules"
                          required
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
                      :rules="requiredFieldRules"
                      required
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                    <v-select
                      solo
                      v-model="editedItem.positionName"
                      label="Position"
                      :items="positions"
                      item-text="name"
                      :rules="requiredFieldRules"
                      required
                    ></v-select>
                  </v-col>
                </v-row>
              </v-container>

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
            <v-card-title class="text-h7 text-center"
              >Are you sure you want to delete this Employee?</v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="closeDelete">Cancel</v-btn>
              <v-btn color="primary" @click="archivateItem">OK</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogArchivate" max-width="500px">
          <v-card>
            <v-card-title class="text-h7 text-center"
              >Would you like to archivate the employee?</v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="removeItemPermanently"
                >No</v-btn
              >
              <v-btn color="primary" @click="archivateItemConfirm">Yes</v-btn>
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
import axios from "axios";
import {EmployeeURL, PositionURL, EmptyGuid} from "../classes/common";

var _employees = new Employee();

export default {
  data: () => ({
    valid: false,
    requiredFieldRules: [
      (v) => !!v || "Required field",
      // (v) =>
      //   (v && v.length > 1 && v.length <= 150) ||
      //   "Position name must have at least 2 characters, but no more than 150",
    ],
    search: "",
    today: new Date().toISOString().substr(0, 10),
    dialog: false,
    editingModeOpen: false,
    dialogDelete: false,
    dialogArchivate: false,
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
    // dialogDelete(val) {
    //   val || this.dialogDelete = false; //TODO: test only popup closing(not by cancel)
    // },
  },

  created() {
    this.initialize();
  },

  methods: {
    initialize() {
      const posRequest = axios.get(PositionURL);
      const emplRequest = axios.get(EmployeeURL);

      axios
        .all([posRequest, emplRequest])
        .then(
          axios.spread((...responses) => {
            this.positions = responses[0].data;
            const tmpEmployees = responses[1].data;

            tmpEmployees.forEach((el) => {
              this.employees.push(_employees.fillMissingData(el, this.positions));
            });
          })
        )
        .catch((errors) => {
          console.error(errors);
        });
    },

    newEmployee() {
      this.editedItem = Object.assign({}, this.defaultItem);
      this.editedIndex = -1;
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

    archivateItem() {
      this.dialogDelete = false;
      this.dialogArchivate = true;
    },

    removeItemPermanently() {
      axios
        .delete(
          EmployeeURL,
          //{ employeeId : this.editedItem.employeeId, removePermanently : true }
          _employees.prepareRemoveRequest(this.editedItem.employeeId)
        )
        .then(() => {
          console.log(
            `Successfully removed employee with id ${this.editedItem.employeeId}`
          );
        })
        .catch((error) => {
          console.error(error);
        });

      this.employees.splice(this.editedIndex, 1);
      this.closeArchivateDialog();
    },

    archivateItemConfirm() {
      axios
        .delete(
          EmployeeURL,
          //{ employeeId : this.editedItem.employeeId, removePermanently : false }
          _employees.prepareArchivateRequest(this.editedItem.employeeId)
        )
        .then(() => {
          console.log(
            `Successfully archivated  employee with id ${this.editedItem.employeeId}`
          );
        })
        .catch((error) => {
          console.error(error);
        });

      this.employees.splice(this.editedIndex, 1);
      this.closeArchivateDialog();
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

    closeArchivateDialog() {
      this.dialogArchivate = false;
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
          this.editExistingEmployee(this.editedItem);
          Object.assign(
            this.employees[this.editedIndex],
            _employees.fillMissingData(this.editedItem, this.positions)
          );
        }
      } else {
        this.addNewEmployee(this.editedItem);
      }
      this.close();
    },

    editExistingEmployee(employee) {
      employee.positionId = _employees.getPossitionIdByName(
        this.positions,
        employee.positionName
      );

      axios
        .post(EmployeeURL, employee)
        .then(() => {
          console.log(
            `Employee with id ${employee.employeeId} was successfully updated`
          );
        })
        .catch((error) => {
          console.error(error);
        });
    },

    addNewEmployee(employee) {
      employee.employeeId = EmptyGuid;
      employee.positionId = _employees.getPossitionIdByName(
        this.positions,
        employee.positionName
      );

      axios.post(EmployeeURL, employee).then((result) => {
        console.log(
          `New employee with id ${result.data} was successfully created`
        );
        employee.employeeId = result.data;
        this.employees.push(
          _employees.fillMissingData(employee, this.positions)
        );
      });
    },
  },
};
</script>

<style scoped>
.formclass {
  background-color: white;
}
</style>