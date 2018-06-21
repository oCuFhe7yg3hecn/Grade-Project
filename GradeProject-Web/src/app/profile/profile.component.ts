import { Component, OnInit } from "@angular/core";
import { ApiService, Profile } from "../Services/api.service";

@Component({
  selector: "app-profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.css"]
})
export class ProfileComponent implements OnInit {
  public profiles: Profile[];

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.apiService
      .getProfiles()
      .subscribe(profiles => (this.profiles = profiles));
  }
}
