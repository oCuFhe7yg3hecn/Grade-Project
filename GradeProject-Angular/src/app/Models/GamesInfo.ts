export class GameInfo {
  constructor(name?: string) {
    this.Name = name;
  }

  public Id: string;

  public Name: string;

  public Description: string;

  public Version: string;

  public Categories: string[];

  public Tags: string[];

  public CoverImageURL: string;

  public MultiMedias: string[];

  public GameUrl: string;

  public Authority: String;

  public CreatedAt: Date;

  public RegistereAt: Date;

  public PlayersCount: number;

  public AvaliablePlatforms: string[];
}
