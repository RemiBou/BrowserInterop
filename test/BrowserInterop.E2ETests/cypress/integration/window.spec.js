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
                        cy.spyFix(w.frames[0].console, 'log', w.frames[0]).as('spy');
                        cy.get('#btn-window-child-frame-console').click();
                        cy.get('@spy').should('be.calledOnce')
                            .and('be.calledWith', "test child");
                    });
            });
    });

    it('window properties', () => {
        cy.window()
            .then(w => {
                cy.get("#btn-window").click();
                cy.get("#window-innerWidth").should("have.text", w.innerWidth.toString());
                cy.get("#window-innerHeight").should("have.text", w.innerHeight.toString());
                cy.get("#window-outerWidth").should("have.text", w.outerWidth.toString());
                cy.get("#window-outerHeight").should("have.text", w.outerHeight.toString());
                cy.get("#window-name").should("have.text", w.name.toString());
                cy.get("#window-personalbar").should("have.text", w.personalbar.visible.toString());
                cy.get("#window-IsSecureContext").should("have.text", w.isSecureContext.toString());
                cy.get("#window-ScreenX").should("have.text", w.screenX.toString());
                cy.get("#window-ScreenY").should("have.text", w.screenY.toString());
                cy.get("#window-ScrollX").should("have.text", w.scrollX.toString());
                cy.get("#window-ScrollY").should("have.text", w.scrollY.toString());
                cy.get("#window-Origin").should("have.text", w.origin);
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

    it('window get parent', () => {
        cy.window()
            .then(w => {
                cy.get("#btn-window-parent").click();
                cy.get("#window-parent-name").should("have.text", w.parent.name.toString());
            });
    });


    it('window viewport', () => {
        cy.window()
            .then(w => {
                if (!w.visualViewport) {
                    return;
                }
                cy.get("#btn-window-visualviewport").click();
                cy.get("#window-viewport-OffsetLeft").should("have.text", w.visualViewport.offsetLeft.toString());
                cy.get("#window-viewport-OffsetTop").should("have.text", w.visualViewport.offsetTop.toString());
                cy.get("#window-viewport-PageLeft").should("have.text", w.visualViewport.pageLeft.toString());
                cy.get("#window-viewport-PageTop").should("have.text", w.visualViewport.pageTop.toString());
                cy.get("#window-viewport-Width").should("have.text", w.visualViewport.width.toString());
                cy.get("#window-viewport-Height").should("have.text", w.visualViewport.height.toString());
                cy.get("#window-viewport-Scale").should("have.text", w.visualViewport.scale.toString());
                w.visualViewport.dispatchEvent(new Event("resize"));
                cy.get("#window-viewport-resizeHandled").should("have.text", '1');

                w.visualViewport.dispatchEvent(new Event("scroll"));
                cy.get("#window-viewport-scrollHandled").should("have.text", '1');

            });
    });
    function windowMethodCallTest(methodName, stub, ...args) {
        it("window " + methodName, () => {
            cy.window()
                .then(w => {
                    cy.stubFix(w, methodName, w, stub).as('spyMethod');
                });
            cy.get("#btn-window-" + methodName).click();
            cy.get('@spyMethod').should('be.calledOnce');
            if (args.length > 0)
                cy.get('@spyMethod').should('be.calledWith', ...args)
        });

    };
    windowMethodCallTest("alert", () => { }, "test");
    windowMethodCallTest("blur", () => { });
    windowMethodCallTest("close", () => { });
    windowMethodCallTest("confirm", () => { return false; }, "test");
    windowMethodCallTest("focus", () => { });
    windowMethodCallTest("moveBy", () => { }, 1, 2);
    windowMethodCallTest("moveTo", () => { }, 3, 4);
    it('window postMessage', () => {
        cy.get("#btn-window-postMessage").click();
        cy.get("#window-message-data").should("have.text", "message");
        cy.get("#window-message-origin").should("have.text", "http://localhost:5000");
        cy.get("#window-message-source").should("have.text", "true");
    });
    it("window open", () => {
        var fakeWindow = {
            close: function () { }
        };
        cy.spy(fakeWindow, 'close').as('spyClose');
        cy.window()
            .then(w => {
                cy.stubFix(w, 'open', w, function () { return fakeWindow; }).as('spyOpen');

            });
        cy.get("#btn-window-open").click().end();
        cy.get('@spyOpen').should('be.calledOnce')
            .and('be.calledWith', "/window", "_blank", "menubar=no")
            .and('returned', fakeWindow);
        cy.get("#btn-window-close-opened").click();
        cy.get('@spyClose').should('be.calledOnce');
    });

});
