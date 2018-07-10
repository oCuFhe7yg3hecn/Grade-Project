import { GameInfo } from './../Models/GamesInfo';
import { Component, Inject, Injectable, OnInit, Input, OnChanges } from "@angular/core";
import {
  Breakpoints,
  BreakpointState,
  BreakpointObserver
} from "@angular/cdk/layout";

@Component({
  selector: "app-items-dashboard",
  templateUrl: "./items-dashboard.component.html",
  styleUrls: ["./items-dashboard.component.css"]
})
export class ItemsDashboardComponent implements OnInit, OnChanges {
  /** Based on the screen size, switch from standard to one column per row */
  cards = [
    { title: '', cols: 2, rows: 1, content: '', imageUrl: '' },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '' },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '' },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '' },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '' },
    { title: '', cols: 1, rows: 2, content: '', imageUrl: '' },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '' },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '' }
  ];

  @Input() DataSrc: GameInfo[];

  constructor(private breakpointObserver: BreakpointObserver) {}

  ngOnInit() {
  }

  populateCards(): void{
    for(let i=0; i<this.DataSrc.length; i++){
      this.cards[i].title = this.DataSrc[i].Name;
      this.cards[i].content = this.DataSrc[i].Description;
      this.cards[i].imageUrl = this.DataSrc[i].CoverImageURL;
    }
  }

  ngOnChanges(){
    this.populateCards();
  }
}
