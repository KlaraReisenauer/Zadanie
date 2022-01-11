const _rooUrl = 'http://localhost:3000/api/';
const _positionPath = 'positions';
const _pastEmployeePath = 'pastemployees';
const _employeePath = 'employees';

export const PositionURL = _rooUrl + _positionPath;
export const PastEmployeeURL = _rooUrl + _pastEmployeePath;
export const EmployeeURL = _rooUrl + _employeePath;
export const EmptyGuid = "00000000-0000-0000-0000-000000000000";

export function prepareRequestData(rdata?: any) {
    return {
        baseURL: _rooUrl,
        data: rdata, //(rdata) ? JSON.stringify(rdata) : undefined,
        xsrfCookieName: 'XSRF-TOKEN',
        xsrfHeaderName: 'X-XSRF-TOKEN',
        headers: { 'Content-Type': 'application/json' }
    };
}