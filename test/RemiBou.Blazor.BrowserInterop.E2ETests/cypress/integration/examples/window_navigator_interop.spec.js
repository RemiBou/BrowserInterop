/// <reference types="Cypress" />

context('window.navigator', () => {
  beforeEach(() => {
    cy.visit('/navigator')
  })

  // https://on.cypress.io/interacting-with-elements

  it('Check window.navigator Interop', () => {
    cy.window().then(w => {
      cy.get("#navigator-app-code-name").should('have.text', w.navigator.appCodeName);
      cy.get("#navigator-app-name").should('have.text', w.navigator.appName);
      cy.get("#navigator-app-version").should('have.text', w.navigator.appVersion);

    });
  });
})
