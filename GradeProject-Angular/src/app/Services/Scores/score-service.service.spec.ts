import { TestBed, inject } from '@angular/core/testing';

import { ScoreServiceService } from './score-service.service';

describe('ScoreServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ScoreServiceService]
    });
  });

  it('should be created', inject([ScoreServiceService], (service: ScoreServiceService) => {
    expect(service).toBeTruthy();
  }));
});
