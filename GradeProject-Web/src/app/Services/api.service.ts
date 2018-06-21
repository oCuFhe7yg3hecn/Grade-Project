import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Url } from "url";
import { of } from "rxjs/observable/of";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class ApiService {
  private profileServiceUrl = "http://localhost:44312";

  constructor(private http: HttpClient) {
    console.log("Http Service Injected");
  }

  getProfiles(): Observable<Profile[]> {
    var request = this.http.get<Profile[]>("https://localhost:44312/api/users");
    request.subscribe(res => console.log(res));
    return request;
  }
}

export class Profile {
  Id: string;
  FirstName: string;
  LastName: string;
  MiddleName: string;
  DOB: string;
  Gender: string;
  ImageURL: Url;
  Rank: string;
  Slogan: string;
  Status: string;
  CurrentAction: string;
  TotalScore: number;
  FavouriteGenres: string[];
  Games: Map<string, number>;
  FriendsList: string[];
}
