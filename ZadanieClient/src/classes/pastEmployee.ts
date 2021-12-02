import { Employee } from "./employee";
import { IEmployee } from "./employee";

export interface IPastEmployee extends IEmployee {
    endDate: string
}

export class PastEmployee {
    private _emplCls;

    constructor(){
        this._emplCls = new Employee();        
    }

    public loadPastEmployees(): IPastEmployee[] {
        const pastEmployees: IPastEmployee[] = [
            {
                id: "8F7DE5AC-769A-44EF-B9F6-1C3AAA0A219B",
                name: "Sirius",
                surname: "Black",
                dateOfBirth: new Date("1980-07-31").toISOString().substr(0, 10),
                salary: 0,
                startDate: new Date("1980-07-31").toISOString().substr(0, 10),
                positionId: 4,
                endDate: new Date("1980-07-31").toISOString().substr(0, 10)
            },
            {
                id: "8C2164E5-5670-4733-B888-2D8F44F6F704",
                name: "Lily",
                surname: "Potter",
                dateOfBirth: new Date("1980-07-31").toISOString().substr(0, 10),
                salary: 0,
                startDate: new Date("1980-07-31").toISOString().substr(0, 10),
                positionId: 3,
                endDate: new Date("1980-07-31").toISOString().substr(0, 10)
            },
            {
                id: "F58C8E6D-2AA7-4FEC-A647-EA336C70BC5F",
                name: "James",
                surname: "Potter",
                dateOfBirth: new Date("1980-07-31").toISOString().substr(0, 10),
                salary: 0,
                startDate: new Date("1980-07-31").toISOString().substr(0, 10),
                positionId: 3,
                endDate: new Date("1980-07-31").toISOString().substr(0, 10)
            }
        ];

        pastEmployees.forEach(el => {
            el.fullname = this._emplCls.createFullName(el.name, el.surname);
            el.positionName = this._emplCls.mapPositionIdToName(el.positionId)
        });

        return pastEmployees;
    }

    // load positions??
    public deletePastEmployee(emplId: string) {
        this._emplCls.deleteEmployee(emplId);
    }

    // function for getting tmpPosition from api
    private getAllEmployees() { }

    //function for mapping result from API to interface
    private mapApiResult() { }

}