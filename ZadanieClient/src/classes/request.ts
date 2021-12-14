import axios from "axios";

export class Request {
    private readonly _rootURL = 'https://localhost:49153/api';

    public prepareRequest(path: string, data?: any) {   //return parsed response??
        const configuration = this.prepareRequestData(data);
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

    private prepareRequestData(rdata?: any) {
        return {
            baseURL: this._rootURL,
            data: rdata,
            xsrfCookieName: 'XSRF-TOKEN',
            xsrfHeaderName: 'X-XSRF-TOKEN',
            headers: { 'Content-Type': 'application/json' }
        };
    }
}