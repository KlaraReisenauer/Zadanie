import { Position } from "./position";
import { IPosition } from "./position";

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
    private readonly _emptyGuid = "00000000-0000-0000-0000-000000000000";

    public isChanged(newEmpl: IEmployee, origEmpl: IEmployee): Boolean {
        if (newEmpl.name !== origEmpl.name || newEmpl.surname !== origEmpl.surname
            || newEmpl.address !== origEmpl.address || newEmpl.positionName !== origEmpl.positionName
            || newEmpl.salary !== origEmpl.salary) {
            return true;
        }

        return false;
    }

    public fillMissingData(employee: IEmployee, positions: IPosition[]): IEmployee {
        employee.fullname = this.createFullName(employee.name, employee.surname);
        employee.positionName = this.getPossitionNameById(positions, employee.positionId);
        return employee;
    }

    public getPossitionIdByName(positions: IPosition[], name: string) {
        return positions.find((p) => p.name === name)?.positionId ??
            1;
    }

    public prepareArchivateRequest(employeeId: string) {
        return {
            data:
            {
                employeeId: employeeId,
                removePermanently: false
            }
        }
    }

    public prepareRemoveRequest(employeeId: string) {
        return {
            data:
            {
                employeeId: employeeId,
                removePermanently: true
            }
        }
    }

    private createFullName(fname: string, lname: string): string {
        return fname + " " + lname;
    }

    private getPossitionNameById(positions: IPosition[], id: number) {
        return positions.find((p) => p.positionId === id)?.name ??
            "";
    }

    //function for mapping result from API to interface
    private mapApiResult(resp: any) {
        const employee: IEmployee = {
            employeeId: resp.employeeId,
            name: resp.name,
            surname: resp.surname,
            fullname: this.createFullName(resp.name, resp.surname),
            address: resp.address ?? undefined,
            dateOfBirth: resp.dateOfBirth, //datepicker expects string, not a date
            startDate: resp.startDate,
            salary: resp.salary, //rounded to 2 decimal places
            positionId: resp.positionId
        };

        return employee;
    }
}