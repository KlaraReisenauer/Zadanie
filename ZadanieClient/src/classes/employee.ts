import { Position } from "./position";
import { IPosition } from "./position";
import { Request } from "./request";
import { RequestType } from "./request";

export interface IEmployee {
    employeeId: string,
    name: string,
    surname: string,
    fullname?: string,
    address?: string,
    dateOfBirth: string, //datepicker expects string, not a date
    startDate: string,
    salary: number, //rounded to 2 decimal places
    positionId: number,
    positionName?: string
}

export class Employee {
    private readonly _path = "Employees";
    private readonly _positions: IPosition[]; // TODO: keep synchronized with changes on positions... HOW??
    private readonly _emptyGuid = "00000000-0000-0000-0000-000000000000";

    constructor() {
        const pos = new Position();
        this._positions = pos.loadPositions();
    }

    public loadEmployees(): IEmployee[] {
        const employees: IEmployee[] = [
            {
                employeeId: "8F7DE5AC-769A-44EF-B9F6-1C3AAA0A219B",
                name: "Sirius",
                surname: "Black",
                dateOfBirth: new Date("1980-07-31").toISOString().substr(0, 10),
                salary: 0,
                startDate: new Date("1980-07-31").toISOString().substr(0, 10),
                positionId: 4,
            },
            {
                employeeId: "8C2164E5-5670-4733-B888-2D8F44F6F704",
                name: "Lily",
                surname: "Potter",
                dateOfBirth: new Date("1980-07-31").toISOString().substr(0, 10),
                salary: 0,
                startDate: new Date("1980-07-31").toISOString().substr(0, 10),
                positionId: 3,
            },
            {
                employeeId: "BBF73A4D-BF6E-4860-BDBE-66ADDD697012",
                name: "Harry",
                surname: "Potter",
                address: "4 Privet Drive, Surrey",
                dateOfBirth: new Date("1980-07-31").toISOString().substr(0, 10),
                salary: 2500.55,
                startDate: new Date("2021-11-19 12:29:59.490").toISOString().substr(0, 10),
                positionId: 1,
            },
            {
                employeeId: "B3A11DB7-7E23-4775-9EBA-B5C98DF78401",
                name: "Hermione",
                surname: "Granger",
                dateOfBirth: new Date("1980-07-31").toISOString().substr(0, 10),
                salary: 0,
                startDate: new Date("1980-07-31").toISOString().substr(0, 10),
                positionId: 5,
            },
            {
                employeeId: "633F0C41-BC3A-4674-A94F-E24531724EDF",
                name: "Ronald",
                surname: "Weasley",
                dateOfBirth: new Date("1980-07-31").toISOString().substr(0, 10),
                salary: 0,
                startDate: new Date("1980-07-31").toISOString().substr(0, 10),
                positionId: 2,
            },
            {
                employeeId: "F58C8E6D-2AA7-4FEC-A647-EA336C70BC5F",
                name: "James",
                surname: "Potter",
                dateOfBirth: new Date("1980-07-31").toISOString().substr(0, 10),
                salary: 0,
                startDate: new Date("1980-07-31").toISOString().substr(0, 10),
                positionId: 3,
            },
        ];

        const emplTest = this.getAllEmployees();

        // TODO: remove when above is working
        employees.forEach(el => {
            el.fullname = this.createFullName(el.name, el.surname);
            el.positionName = this.mapPositionIdToName(el.positionId);
        });

        return employees;
    }

    public loadPositions() { //return loaded positions to UI
        return this._positions;
    }

    public addNewEmployee(empl: IEmployee): IEmployee {
        if (empl.positionName) {
            empl.positionId = this.mapPositionNameToId(empl.positionName);
        }
        empl.employeeId = this._emptyGuid; // TODO: getting new id as result from API call
        empl.fullname = this.createFullName(empl.name, empl.surname);

        this.saveEmployee(empl);

        return empl;
    }

    public isChanged(newEmpl: IEmployee, origEmpl: IEmployee): Boolean {
        if (newEmpl.name !== origEmpl.name || newEmpl.surname !== origEmpl.surname
            || newEmpl.address !== origEmpl.address || newEmpl.positionId !== origEmpl.positionId
            || newEmpl.salary !== origEmpl.salary) {
            return true;
        }

        return false;
    }

    public deleteAndArchiveEmployee(emplId: string) { //TODO: add this logic to ui
        this.removeEmployee(emplId, false);
    }

    public deleteEmployee(emplId: string) {
        this.removeEmployee(emplId, true);
    }

    public editEmployee(empl: IEmployee): IEmployee {
        if (empl.positionName) {
            empl.positionId = this.mapPositionNameToId(empl.positionName);
        }
        empl.fullname = this.createFullName(empl.name, empl.surname);

        this.saveEmployee(empl);

        return empl;
    }

    public mapPositionIdToName(posId: number): string {
        return this._positions.find(p => p.PositionId === posId)?.name ?? ""; // TODO: log error if not find?
    }

    public createFullName(fname: string, lname: string): string {
        return fname + " " + lname;
    }

    private mapPositionNameToId(posName: string): number {
        return this._positions.find(p => p.name === posName)?.PositionId ?? 0; // TODO: log error if not find?
    }

    private getAllEmployees() {
        const request = new Request();
        const response = request.prepareRequest(this._path, RequestType.Get);
    }

    private saveEmployee(empl: IEmployee) {
        const request = new Request();
        const response = request.prepareRequest(this._path, RequestType.Post,
            JSON.stringify(empl));
    }

    private removeEmployee(emplId: string, removePermanently: boolean) {
        const request = new Request();
        const response = request.prepareRequest(this._path, RequestType.Delete,
            JSON.stringify({
                employeeId: emplId,
                removePermanently: removePermanently,
            }));
    }

    //function for mapping result from API to interface
    private mapApiResult() { }
}