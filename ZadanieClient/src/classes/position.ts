import axios from "axios";
import { PositionURL } from "../classes/common";

export interface IPosition {
    positionId: number;
    name: string;
}

export class Position {
    private readonly _path = "Positions";

    public loadPositions() {
        axios.get<IPosition[]>(PositionURL).then((result) => {
            return result.data;
        }).catch((error) => {
            console.error("Unsuccessfull retrieval of position data");
            console.error(error);
        });
    }

}


