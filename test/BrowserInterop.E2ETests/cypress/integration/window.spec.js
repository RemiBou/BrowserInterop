/// <reference types="Cypress" />

context('window.navigator', () => {
    before(() => {
        cy.visit('/window');
    });
    it('Call child frame method', () => {
        cy.get('#child-iframe')
            .iframe()
            .then((i) => {
                i.contents().find('h1');
                cy.window()
                    .then(w => {
                        console.log(window.origin, w.origin, w.frames[0].origin);
                        cy.spyFix(w.frames[0].console, 'log', w.frames[0]);
                        cy.get('#btn-window-child-frame-console').click().then(
                            () => {
                                expect(w.frames[0].console.log).to.be.calledOnce
                                expect(w.frames[0].console.log).to.be.calledWith("test child");
                            }
                        )
                    });
            });
    });

    it('window properties', () => {
        cy.window()
            .then(w => {
                cy.get("#window-innerWidth").should("have.text", w.innerWidth.toString());
                cy.get("#window-innerHeight").should("have.text", w.innerHeight.toString());
            });
    });
});
