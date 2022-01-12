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
    positionName?: string,
    endDate?: string,
}

export class Employee {
    public isChanged(newEmpl: IEmployee, origEmpl: IEmployee): Boolean {
        if (newEmpl.name !== origEmpl.name || newEmpl.surname !== origEmpl.surname
            || newEmpl.address !== origEmpl.address || newEmpl.positionName !== origEmpl.positionName
            || newEmpl.salary !== origEmpl.salary || newEmpl.startDate !== origEmpl.startDate) {
            return true;
        }

        return false;
    }

    public fillMissingData(employee: IEmployee, positions: IPosition[]): IEmployee {
        employee.fullname = this.createFullName(employee.name, employee.surname);
        employee.positionName = this.getPossitionNameById(positions, employee.positionId);
        employee.dateOfBirth = employee.dateOfBirth.substring(0,10);
        employee.startDate = employee.startDate.substring(0,10);
        if(employee.endDate){
            employee.endDate = employee.endDate.substring(0,10);
        }
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
}