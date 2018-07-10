import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GameInfo } from '../Models/GamesInfo';
import { OData } from '../Models/Odata';

@Injectable({
  providedIn: "root"
})
export class GamesService {
  constructor(private http: HttpClient) {}

  GetGamesByGenre(): Observable<OData<GameInfo>>{
      var res = this.http.get<OData<GameInfo>>("http://localhost:54554//odata//Games");
      return res;
  }
}