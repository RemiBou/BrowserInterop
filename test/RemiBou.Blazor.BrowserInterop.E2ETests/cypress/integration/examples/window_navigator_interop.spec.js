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
      cy.get("#navigator-connection-downlink").should('have.text', w.navigator.connection.downlink.toString());
      cy.get("#navigator-connection-effectiveType").should('have.text', w.navigator.connection.effectiveType.toString());

      if ('downlinkMax' in w.navigator.connection) {// this property is not available in every browser
        cy.get("#navigator-connection-downlinkmax").should('have.text', w.navigator.connection.downlinkMax.toString());
      }
    });
  });
})
