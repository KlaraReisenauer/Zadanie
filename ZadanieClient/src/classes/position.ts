import { Request } from "./request";
import { RequestType } from "./request";

export interface IPosition {
    PositionId: number;
    name: string;
}

export class Position {
    private readonly _path = "Positions";

    public loadPositions(): IPosition[] {
        const tmpPositions = [ //TODO: replace with api call
            {
                PositionId: 1,
                name: "Ine"
            },
            {
                PositionId: 2,
                name: "Tester",
            },
            {
                PositionId: 3,
                name: "Programator",
            },
            {
                PositionId: 4,
                name: "Support",
            },
            {
                PositionId: 5,
                name: "Analytik",
            },
            {
                PositionId: 6,
                name: "Obchodnik",
            }];
        const positions = this.getAllPositions();        

        return tmpPositions;
    }

    public addNewPosition(positionName: string): IPosition {
        //todo call API to assign PositionId
        const newPosition: IPosition = {
            name: positionName,
            PositionId: 0
        };
        
        this.savePosition(newPosition); //TODO: return updated position

        return newPosition;
    }

    public deletePosition(positionId: number) {

        this.removePosition(positionId);
    }

    public editPosition(position: IPosition) {
        this.savePosition(position);
    }

    // function for getting tmpPosition from api
    private getAllPositions() {
        const request = new Request();
        const result = request.prepareRequest(this._path, RequestType.Get);
     }

    // function for sending data to API for save
    private savePosition(position: IPosition) {
        const request = new Request();
        const result = request.prepareRequest(this._path, RequestType.Post,
            JSON.stringify(position));
     }

    // function for sending data to API for deleting position
    private removePosition(positionId: number) {
        const request = new Request();
        const result = request.prepareRequest(this._path, RequestType.Delete,
            JSON.stringify({
                positionId: positionId
            }));
     }
    
    //function for mapping result from API to interface
    private mapApiResult() { }

}


