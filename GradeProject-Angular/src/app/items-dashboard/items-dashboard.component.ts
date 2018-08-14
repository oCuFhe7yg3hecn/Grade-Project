import { DataSource } from '@angular/cdk/collections';
import { GameInfo } from '../Models/GamesInfo';
import { Component, Inject, Injectable, OnInit, Input, OnChanges, SimpleChanges, SimpleChange } from "@angular/core";
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
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '', visible: true },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '', visible: true },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '', visible: true },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '', visible: true },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '', visible: true },
    { title: '', cols: 1, rows: 2, content: '', imageUrl: '', visible: true },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '', visible: true },
    { title: '', cols: 1, rows: 1, content: '', imageUrl: '', visible: true }
  ];

  @Input() DataSrc: GameInfo[];

  constructor(private breakpointObserver: BreakpointObserver) {}

  ngOnInit() {
  }



  populateCards(): void{
    this.cards.forEach(card => {
      card.title = '';
      card.imageUrl = '';
      card.content = '';
      card.visible = false;
    });
    for(let i=0; i<this.DataSrc.length; i++){
      this.cards[i].title = this.DataSrc[i].Name;
      this.cards[i].content = this.DataSrc[i].Description;
      this.cards[i].imageUrl = this.DataSrc[i].CoverImageURL;
      this.cards[i].visible = true;
    }
  }

  ngOnChanges(changes: SimpleChanges){
    // Try it later
    // const dataSourceChange: SimpleChange = changes.DataSrc;
    // console.log('previous : ', dataSourceChange.previousValue);
    // console.log('current : ', dataSourceChange.currentValue);

    // var newIds = dataSourceChange.currentValue.map(x => x.Id)
    // this.cards = [];
    // console.log('new Ids : ', newIds);
    // // this.DataSrc = dataSourceChange.currentValue;
    this.populateCards();
  }
}
