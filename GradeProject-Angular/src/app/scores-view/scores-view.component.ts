import { ScoreServiceService } from './../Services/Scores/score-service.service';
import { Component, OnInit } from '@angular/core';

export interface IScoreData{
  name: string;
  score: number;
}

@Component({
  selector: 'app-scores-view',
  templateUrl: './scores-view.component.html',
  styleUrls: ['./scores-view.component.css']
})

export class ScoresViewComponent implements OnInit {

  displayedColumns: string[] = ['name', 'score'];
  myDataArray: IScoreData[] = [{name: 'test', score: 22.15}]; 

  constructor(private scoreSvc:ScoreServiceService) { }

  ngOnInit() {
    debugger;
    this.scoreSvc.getUserScores('bfe28474-8297-4638-bcc9-14a7f3a9a7d3');
  }

}
