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
            cy.get("#navigator-connection-rtt").should('have.text', w.navigator.connection.rtt.toString());
            cy.get("#navigator-connection-saveData").should('have.text', w.navigator.connection.saveData.toString());

            if ('downlinkMax' in w.navigator.connection) {// this property is not available in every browser
                cy.get("#navigator-connection-downlinkmax").should('have.text', w.navigator.connection.downlinkMax.toString());
            }
            cy.get("#navigator-connection-type").should('have.text', w.navigator.connection.type ? w.navigator.connection.type : '');
            cy.get("#navigator-cookieEnabled").should('have.text', w.navigator.cookieEnabled.toString());
            cy.get("#navigator-hardwareConcurrency").should('have.text', w.navigator.hardwareConcurrency.toString());
            cy.get("#navigator-javaEnabled").should('have.text', w.navigator.javaEnabled.toString());
            cy.get("#navigator-language").should('have.text', w.navigator.language);

        });
    });
})
