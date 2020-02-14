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
                cy.get("#window-outerWidth").should("have.text", w.outerWidth.toString());
                cy.get("#window-outerHeight").should("have.text", w.outerHeight.toString());
                cy.get("#window-name").should("have.text", w.name.toString());
                cy.get("#window-personalbar").should("have.text", w.personalbar.visible.toString());
            });
    });

    it('window set name', () => {
        cy.window()
            .then(w => {
                cy.get("#btn-window-name-set").click().then(
                    () => expect(w.name).to.be.eq("Bla bla bla")
                );
            });
    });

    /*
        it('window get opener', () => {
            cy.window()
                .then(w => {
                    cy.get("#btn-window-opener").click().then(
                        () => cy.get("#window-opener-name").should("have.text", w.opener.name.toString())
                    );
                });
        });*/


    it('window get parent', () => {
        cy.window()
            .then(w => {
                cy.get("#btn-window-parent").click().then(
                    () => cy.get("#window-parent-name").should("have.text", w.parent.name.toString())
                );
            });
    });
});
