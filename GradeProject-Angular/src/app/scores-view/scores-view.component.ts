import { IScore } from './../Services/Scores/IScore';
import { UserScore } from './../Services/Scores/UserScore';
import { IScoreData } from './IScoreData';
import { HttpClient } from "@angular/common/http";
import { ScoreServiceService } from "./../Services/Scores/score-service.service";
import { Component, OnInit } from "@angular/core";

export interface IScoreData {
  name: string;
  score: number;
}

@Component({
  selector: "app-scores-view",
  templateUrl: "./scores-view.component.html",
  styleUrls: ["./scores-view.component.css"]
})
export class ScoresViewComponent implements OnInit {
  displayedColumns: string[] = ["name", "score"];
  myDataArray: IScore[] = [{ name: "test", score: 22.15 }];

  constructor(
    private http: HttpClient,
    private scoreSvc: ScoreServiceService
  ) {}

  ngOnInit() {

    this.scoreSvc
      .getUserScores("bfe28474-8297-4638-bcc9-14a7f3a9a7d3")
      .subscribe(res => {
        console.log(res);
        this.http
          .get<string>(`https://localhost:44312/api/Players/${res.userId}/name`)
          .subscribe(name => {
            res.userId = name;
            this.myDataArray = res.scores;
          });
      });
  }
}
