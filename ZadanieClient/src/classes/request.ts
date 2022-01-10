import axios from 'axios';

export enum RequestType {
    Post = "SAVE",
    Delete = "DELETE",
    Get = "GET",
}
export class Request {
    private readonly _rootURL = 'http://localhost:3000/api';

    public prepareRequest(path: string, type: string, data?: any) {   //return parsed response??
        const configuration = this.prepareRequestData(data);
        switch (type) {
            case RequestType.Get:
                return this.sendGetRequest(path, configuration);
                break;
            case RequestType.Post:
                return this.sendPostRequest(path, configuration);
                break;
            case RequestType.Delete:
                return this.sendDeleteRequest(path, configuration);
                break;
        }
    }

    private async sendGetRequest(path: string, conf: any) {
        const http = axios.create(conf);
        http.get(`${path}`)
            .then((response) => {
                console.log(response); // TODO: Remove before finish
                return response.data;
            }, (error) => {
                console.error(error);
            });
    }

    private async sendPostRequest(path: string, conf: any) {
        const http = axios.create(conf);
        http.post(`${path}`)
            .then((response) => {
                console.log(response); // TODO: Remove before finish
                return response.data;
            }, (error) => {
                console.error(error);
            });
    }

    private async sendDeleteRequest(path: string, conf: any) {
        const http = axios.create(conf);
        http.delete(`${path}`)
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