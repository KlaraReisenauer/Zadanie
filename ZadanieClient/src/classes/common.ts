const _rooUrl = 'http://localhost:3000/api/';
const _positionPath = 'positions';
const _pastEmployeePath = 'pastemployees';
const _employeePath = 'employees';

export const PositionURL = _rooUrl + _positionPath;
export const PastEmployeeURL = _rooUrl + _pastEmployeePath;
export const EmployeeURL = _rooUrl + _employeePath;
export const EmptyGuid = "00000000-0000-0000-0000-000000000000";

export function handleErrorMsg(error : any) : string {
    let status, msg;

    if(error.response.data.title){
        status = error.response.data.status;
        msg = error.response.data.title;
    }
    else{
        status = error.response.status;
        msg = error.response.data;
    }
    return "Error " + status + ": " + msg;
}