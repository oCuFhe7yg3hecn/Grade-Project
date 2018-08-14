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

  getUserScores(userId: string): Observable<UserScore> {
    return this.http
      .get<UserScore>(`https://localhost:44350/api/Scores/user/${userId}`);
  }

  getGameScores(gameId: string): Observable<IScoreData[]> {
    return this.http
      .get<IScoreData[]>(`https://localhost:44350/api/Scores/game/${gameId}`);
  }

  getOverAllScores() {}
}