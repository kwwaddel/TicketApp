import { MovieWebAppPage } from './app.po';

describe('movie-web-app App', function() {
  let page: MovieWebAppPage;

  beforeEach(() => {
    page = new MovieWebAppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
