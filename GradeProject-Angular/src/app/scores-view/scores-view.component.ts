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

  constructor() { }

  ngOnInit() {
  }

}
