import { Component } from "@angular/core";
import { map } from "rxjs/operators";
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
export class ItemsDashboardComponent {
  /** Based on the screen size, switch from standard to one column per row */
  cards = [
    { title: "Pacman", cols: 2, rows: 1, Content: "" },
    { title: "Card 2", cols: 1, rows: 1, Content: "" },
    { title: "Card 3", cols: 1, rows: 1, Content: "" },
    { title: "Card 4", cols: 1, rows: 1, Content: "" },
    { title: "Card 4", cols: 1, rows: 1, Content: "" }
  ];

  constructor(private breakpointObserver: BreakpointObserver) {}
}
