import { GamesService } from "../Services/games-service.service";
import { Component, OnInit } from "@angular/core";
import { GameInfo } from "../Models/GamesInfo";

@Component({
  selector: "app-games-catalog",
  templateUrl: "./games-catalog.component.html",
  styleUrls: ["./games-catalog.component.css"]
})
export class GamesCatalogComponent implements OnInit {
  public Genre: string = "Genre";
  public Games: GameInfo[];
  public nameFilter : string;

  constructor(private gamesSvc: GamesService) {}

  searchByName(){
    // this.Games = [];
    this.gamesSvc.GetByName(this.nameFilter).subscribe(res => this.Games = res.value);
  }


  ngOnInit() {
    //Should send cards Absolutely
    this.gamesSvc.GetGamesByGenre().subscribe(res => {
      this.Games = res.value;
      console.log(this.Games);
    });
  }
}
 