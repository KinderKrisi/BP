import { TestBed } from '@angular/core/testing';

import { LogsendService } from './logsend.service';

describe('LogsendService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LogsendService = TestBed.get(LogsendService);
    expect(service).toBeTruthy();
  });
});
