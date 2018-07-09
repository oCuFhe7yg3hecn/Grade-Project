import { MaterialsImportModule } from './materials-import.module';

describe('MaterialsImportModule', () => {
  let materialsImportModule: MaterialsImportModule;

  beforeEach(() => {
    materialsImportModule = new MaterialsImportModule();
  });

  it('should create an instance', () => {
    expect(materialsImportModule).toBeTruthy();
  });
});
