export interface IPosition {
    id: number;
    name: string;
    removedOn?: Date;
}

export class Position {
    public loadPositions(): IPosition[] {
        const tmpPositions = [ //TODO: replace with api call
            {
                id: 1,
                name: "Ine"
            },
            {
                id: 2,
                name: "Tester",
            },
            {
                id: 3,
                name: "Programator",
            },
            {
                id: 4,
                name: "Support",
            },
            {
                id: 5,
                name: "Analytik",
            },
            {
                id: 6,
                name: "Obchodnik",
            }];
        //let positions = this.getAllPositions();        

        return tmpPositions;
    }

    public addNewPosition(positionName: string): IPosition {
        //todo call API to assign ID
        const newPosition: IPosition = {
            name: positionName,
            id: 0
        };
        this.savePosition(newPosition); //TODO: return updated position

        return newPosition;
    }

    public deletePosition(position: IPosition) {
        position.removedOn = new Date();
        this.removePosition(position);
    }

    public editPosition(position: IPosition) {
        this.savePosition(position);
    }

    // function for getting tmpPosition from api
    private getAllPositions() { }

    // function for sending data to API for save
    private savePosition(position: IPosition) { }

    // function for sending data to API for deleting position
    private removePosition(position: IPosition) { }

    private prepareRequest(position: IPosition, requestType: string) {

    }
    
    //function for mapping result from API to interface
    private mapApiResult() { }

}


