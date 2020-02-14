/// <reference types="Cypress" />

context('window.storage', () => {
    before(() => {
        cy.visit('/screen');
    });

    it('screen properties', function () {
        cy.window()
            .then(w => {
                cy.get("#window-screen-AvailWidth").should('have.text', w.screen.availWidth.toString());
                cy.get("#window-screen-AvailHeight").should('have.text', w.screen.availHeight.toString());
                cy.get("#window-screen-ColorDepth").should('have.text', w.screen.colorDepth.toString());
                cy.get("#window-screen-Height").should('have.text', w.screen.height.toString());
                cy.get("#window-screen-Orientation-Type").should('have.text', w.screen.orientation.type);
                cy.get("#window-screen-Orientation-TypeEnum").should('not.be.empty');
                cy.get("#window-screen-Orientation-Angle").should('have.text', w.screen.orientation.angle.toString());
                cy.get("#window-screen-PixelDepth").should('have.text', w.screen.pixelDepth.toString());
                cy.get("#window-screen-Width").should('have.text', w.screen.width.toString());
            });
    });
    it('screen lock', function () {
        cy.window()
            .then(w => {
                cy.spyFix(w.screen.orientation, 'lock', w);
                cy.get("#btn-window-screen-lock")
                    .click()
                    .then(
                        () => {
                            expect(w.screen.orientation.lock).to.be.calledOnce
                            expect(w.screen.orientation.lock).to.be.calledWith("landscape-primary");
                        }
                    );

            });
    });

    it('screen unlock', function () {
        cy.window()
            .then(w => {
                cy.spyFix(w.screen.orientation, 'unlock', w);
                cy.get("#btn-window-screen-unlock")
                    .click()
                    .then(
                        () => expect(w.screen.orientation.unlock).to.be.calledOnce
                    );

            });
    });


    it('screen onchange', function () {
        cy.window()
            .then(w => {
                w.screen.orientation.dispatchEvent(new Event("change"));
                cy.get('#window-screen-changeHandled').should('have.text', '1');

            });
    });
});