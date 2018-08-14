import { ScoresViewComponent } from './scores-view/scores-view.component';
import { GamesCatalogComponent } from './games-catalog/games-catalog.component';
import { Routes } from "@angular/router";

export const routes: Routes = [
    { path: 'games', component: GamesCatalogComponent },
    { path: 'scores', component: ScoresViewComponent},
  ];