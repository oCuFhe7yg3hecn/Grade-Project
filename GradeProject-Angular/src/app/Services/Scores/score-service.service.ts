import { Observable } from "rxjs";
import { IScore } from "./IScore";
import { IScoreData } from "./../../scores-view/scores-view.component";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserScore } from "./UserScore";

@Injectable({
  providedIn: "root"
})
export class ScoreServiceService {
  constructor(private http: HttpClient) {}

  getUserScores(userId: string) {
    return this.http
      .get<UserScore>(`https://localhost:44350/api/Scores/user/${userId}`)
      .subscribe(res => {
        debugger;
        console.log(res);
        this.http
          .get<string>(
            `https://localhost:44312/api/Players/${res.userId}/name`
          )
          .subscribe(name => {
            res.userId = name;
          });
      });
  }

  getGameScores(gameId: string): IScore[] {
    let res: IScore[];
    this.http
      .get<IScoreData[]>(`https://localhost:44350/api/Scores/game/${gameId}`)
      .subscribe(scores => {
        console.log(scores);
      });

    return res;
  }

  getOverAllScores() {}
}
