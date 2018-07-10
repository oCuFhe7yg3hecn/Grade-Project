import { GamesService } from "./../Services/games-service.service";
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

  constructor(private gamesSvc: GamesService) {}

  ngOnInit() {
    this.gamesSvc.GetGamesByGenre().subscribe(res => {
      this.Games = res.value;
      console.log('Result' , res);
      console.log('Games :');
      console.log(this.Games);
    });
  }
}
