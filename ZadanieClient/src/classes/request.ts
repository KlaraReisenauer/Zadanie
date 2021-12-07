import axios from "axios";

export class Request {
    private readonly _rootURL = 'https://localhost:49155';

    public prepareRequest(path: string, operation: string, data?: any) {   //return parsed response??
        const configuration = this.prepareRequestData(operation, data);
        return this.sendGetRequest(path, configuration); //(data) ? this.sendPostRequest(path, configuration) :
    }

    private async sendGetRequest(path : string, conf: any) {
        const http = axios.create(conf);
        http.get(`${path}`)
            .then((response) => {
                console.log(response); // TODO: Remove before finish
                return response.data;
            }, (error) => {
                console.error(error);                
            });
    }

    private async sendPostRequest(path : string, conf : any) {
        const http = axios.create(conf);
        http.post(`${path}`)
            .then((response) => {
                console.log(response); // TODO: Remove before finish
                return response.data;
            }, (error) => {
                console.error(error);                
            });
    }

    private prepareRequestData(op: string, rdata?: any) {
        return {
            baseURL: this._rootURL,
            data: {
                operation: op,
                data: rdata
            },
            xsrfCookieName: 'XSRF-TOKEN',
            xsrfHeaderName: 'X-XSRF-TOKEN',
            headers: { 'Content-Type': 'application/json' }
        };
    }
}