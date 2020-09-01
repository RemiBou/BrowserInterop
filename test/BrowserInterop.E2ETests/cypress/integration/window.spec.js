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
    windowMethodCallTest("print", () => { });
    windowMethodCallTest("prompt", () => { return "text"; }, "message", "return");
    windowMethodCallTest("moveBy", () => { }, 1, 2);
    windowMethodCallTest("moveTo", () => { }, 3, 4);
    windowMethodCallTest("cancelAnimationFrame", () => { }, 1);
    windowMethodCallTest("resizeBy", () => { }, 100, 200);
    windowMethodCallTest("resizeTo", () => { }, 150, 250);
    windowMethodCallTest("scroll", () => { }, 150, 250);
    windowMethodCallTest("stop", () => { });
    windowMethodCallTest("scrollBy", () => { }, { top: 150, left: 250, behavior: 'smooth' });
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
    it('window requestAnimationFrame', () => {
        cy.get("#btn-window-requestAnimationFrame").click();
        cy.get("#window-requestAnimationFrame").should("have.text", "1");
    })
    it('window requestIdleCallback', () => {
        cy.get("#btn-window-requestIdleCallback").click();
        cy.get("#window-requestIdleCallback").should("have.text", "1");
        cy.get("#window-requestIdleCallbackTimeout").should("not.be.empty");
    });
    function windowEventTest(eventName) {
        it("window " + eventName, () => {
            cy.window()
                .then(w => {
                    w.dispatchEvent(new Event(eventName))
                });
            cy.get("#window-event-" + eventName).should('exist');
        });
    }
    windowEventTest('appinstalled');
    //this test must stay comented as it makes cypress test fail because it's an error
    //windowEventTest('error');
    windowEventTest('languagechange');
    windowEventTest('orientationchange');
    windowEventTest('hashchange');
    it('window-onbeforeinstallprompt',
        () => {
            cy.window()
                .then(w => {
                    const event = new Event("beforeinstallprompt");
                    event.platforms = ["test", "test2"];
                    event.userChoice = w.Promise.resolve("accepted");
                    event.prompt = function () {

                    }
                    cy.spy(event, 'prompt').as('spyPrompt');
                    w.dispatchEvent(event);
                    cy.get("#window-event-onbeforeinstallprompt-platforms").should("have.text", "test,test2");
                    cy.get("#window-event-onbeforeinstallprompt-userChoice").should("have.text", "true");
                    cy.get('@spyPrompt').should('be.calledOnce');

                })
        });

    it('window-ondevicemotion',
        () => {
            cy.window()
                .then(w => {
                    const event = new DeviceMotionEvent("devicemotion", {
                        acceleration: {
                            x: 100,
                            y: 150,
                            z: 200
                        },
                        accelerationIncludingGravity: {
                            x: 101,
                            y: 151,
                            z: 201
                        },
                        rotationRate: {
                            alpha: 251,
                            beta: 301,
                            gamma: 351
                        },
                        interval: 162
                    });
                    w.dispatchEvent(event);
                    cy.get("#window-event-ondevicemotion-acceleration-x").should("have.text", "100");
                    cy.get("#window-event-ondevicemotion-acceleration-y").should("have.text", "150");
                    cy.get("#window-event-ondevicemotion-acceleration-z").should("have.text", "200");
                    cy.get("#window-event-ondevicemotion-accelerationIncludingGravity-x").should("have.text", "101");
                    cy.get("#window-event-ondevicemotion-accelerationIncludingGravity-y").should("have.text", "151");
                    cy.get("#window-event-ondevicemotion-accelerationIncludingGravity-z").should("have.text", "201");
                    cy.get("#window-event-ondevicemotion-rotationRate-alpha").should("have.text", "251");
                    cy.get("#window-event-ondevicemotion-rotationRate-beta").should("have.text", "301");
                    cy.get("#window-event-ondevicemotion-rotationRate-gamma").should("have.text", "351");
                    cy.get("#window-event-ondevicemotion-interval").should("have.text", "162");

                })
        });


    it('window-ondeviceorientation',
        () => {
            cy.window()
                .then(w => {
                    const event = new DeviceOrientationEvent("deviceorientation", {
                        alpha: 251,
                        beta: 301,
                        gamma: 351,
                        absolute: true
                    });
                    w.dispatchEvent(event);
                    cy.get("#window-event-ondeviceorientation-alpha").should("have.text", "251");
                    cy.get("#window-event-ondeviceorientation-beta").should("have.text", "301");
                    cy.get("#window-event-ondeviceorientation-gamma").should("have.text", "351");
                    cy.get("#window-event-ondeviceorientation-absolute").should("have.text", "true");

                })
        });

    it('window-onbeforeunload',
        () => {
            cy.window()
                .then(w => {
                    const event = new Event("beforeunload");
                    w.dispatchEvent(event);
                    cy.get("#window-event-beforeunload").should('exist');

                })
        });
    windowEventTest('afterprint');
    windowEventTest('beforeprint');
    windowEventTest('blur');
    windowEventTest('close');
    windowEventTest('focus');
    windowEventTest('load');
    it('window-oncontextmenu',
        () => {
            cy.window()
                .then(w => {
                    const event = new Event("contextmenu");
                    event.preventDefault = function () {

                    }
                    cy.spy(event, 'preventDefault').as('spyPreventDefault');
                    w.dispatchEvent(event);
                    cy.get("#window-event-contextmenu").should('exist');
                    cy.get('@spyPreventDefault').should('be.calledOnce');

                })
        });
    windowEventTest('offline');
    windowEventTest('online');
    it('window-onpagehide',
        () => {
            cy.window()
                .then(w => {
                    const event = new PageTransitionEvent("pagehide", {
                        persisted: true
                    });
                    w.dispatchEvent(event);
                    cy.get("#window-event-pagehide-persisted").should("have.text", "true");

                })
        });
    it('window-onpageshow',
        () => {
            cy.window()
                .then(w => {
                    const event = new PageTransitionEvent("pageshow", {
                        persisted: true
                    });
                    w.dispatchEvent(event);
                    cy.get("#window-event-pageshow-persisted").should("have.text", "true");

                })
        });
    it('window-onpopstate',
        () => {
            cy.window()
                .then(w => {
                    const event = new PopStateEvent("popstate", {
                        state: {
                            test: "aaa"
                        }
                    });
                    w.dispatchEvent(event);
                    cy.get("#window-event-popstate-test").should("have.text", "aaa");

                })
        });
    windowEventTest('resize');
    windowEventTest('scroll');
    it('window-onwheel',
        () => {
            cy.window()
                .then(w => {
                    const event = new WheelEvent("wheel", {
                        deltaX: 101,
                        deltaY: 102,
                        deltaZ: 103,
                        deltaMode: 1,
                    });
                    w.dispatchEvent(event);
                    cy.get("#window-event-wheel-deltaX").should("have.text", "101");
                    cy.get("#window-event-wheel-deltaY").should("have.text", "102");
                    cy.get("#window-event-wheel-deltaZ").should("have.text", "103");
                    cy.get("#window-event-wheel-deltaMode").should("have.text", "Line");

                })
        });

    it('window-onstorage',
        () => {
            cy.window()
                .then(w => {
                    w.sessionStorage.clear();
                    var event = new StorageEvent("storage", {
                        key: "testevent",
                        newValue: "bbb",
                        oldValue: "aaa",
                        storageArea: w.sessionStorage
                    })
                    w.dispatchEvent(event);
                    cy.get("#window-event-storage-oldValue").should("have.text", "aaa");
                    cy.get("#window-event-storage-newValue").should("have.text", "bbb");
                    cy.get("#window-event-storage-key").should("have.text", "testevent").then(() => {
                        expect(w.sessionStorage.getItem('testcallback')).to.be.eq("\"abc\"")
                    });

                })
        });
    windowEventTest('unload');
});

