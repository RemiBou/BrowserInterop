/// <reference types="Cypress" />

context('window.navigator', () => {
    before(() => {
        cy.visit('/navigator')
    })

    // https://on.cypress.io/interacting-with-elements

    it('Check window.navigator properties', () => {
        cy.window().then(w => {
            cy.get("#navigator-app-code-name").should('have.text', w.navigator.appCodeName);
            cy.get("#navigator-app-name").should('have.text', w.navigator.appName);
            cy.get("#navigator-app-version").should('have.text', w.navigator.appVersion);
            cy.get("#navigator-cookieEnabled").should('have.text', w.navigator.cookieEnabled.toString());
            cy.get("#navigator-hardwareConcurrency").should('have.text', w.navigator.hardwareConcurrency.toString());
            cy.get("#navigator-javaEnabled").should('have.text', w.navigator.javaEnabled().toString());
            cy.get("#navigator-language").should('have.text', w.navigator.language);
            w.navigator.languages.forEach(lang => {
                cy.get("#navigator-languages li[lang='" + lang + "']").should("exist");
            });
            cy.get("#navigator-maxTouchPoints").should('have.text', w.navigator.maxTouchPoints.toString());

            cy.get("#navigator-online").should('have.text', w.navigator.onLine.toString());
            cy.get("#navigator-platform").should('have.text', w.navigator.platform);
            cy.get("#navigator-userAgent").should('have.text', w.navigator.userAgent);

        });
    });
    it('Check window.navigator.connection properties', () => {
        cy.window().then(w => {
            cy.get("#navigator-connection-downlink").should('have.text', w.navigator.connection.downlink.toString());
            cy.get("#navigator-connection-effectiveType").should('have.text', w.navigator.connection.effectiveType.toString());
            cy.get("#navigator-connection-rtt").should('have.text', w.navigator.connection.rtt.toString());
            cy.get("#navigator-connection-saveData").should('have.text', w.navigator.connection.saveData.toString());
            cy.get("#navigator-connection-type").should('have.text', w.navigator.connection.type ? w.navigator.connection.type : '');
            cy.get("#navigator-connection-event-change-handled").should('have.text', '0').then(() => {
                w.navigator.connection.dispatchEvent(new Event("change"));
                cy.get("#navigator-connection-event-change-handled").should('have.text', '1').then(() => {
                    cy.get("#navigator-connection-event-change-stop").click().then(() => {
                        w.navigator.connection.dispatchEvent(new Event("change"));
                        cy.get("#navigator-connection-event-change-handled").should('have.text', '1');
                    });
                });
            });
        });
    });
    it('Check non standard properties', () => {
        cy.window().then(w => {
            if ('downlinkMax' in w.navigator.connection) {// this property is not available in every browser
                cy.get("#navigator-connection-downlinkmax").should('have.text', w.navigator.connection.downlinkMax.toString());
            }
        });
    });
    it('Check navigator plugins', () => {
        cy.window().then(w => {
            for (let index = 0; index < w.navigator.plugins.length; index++) {
                const element = w.navigator.plugins[index];
                cy.get("#navigator-plugins li[filename='" + element.filename + "']").should("exist");

            }
        });
    });
    it('Check navigator mimeTypes', () => {
        cy.window().then(w => {
            for (let index = 0; index < w.navigator.mimeTypes.length; index++) {
                const element = w.navigator.mimeTypes[index];
                cy.get("#navigator-mimeTypes li[mime='" + element.type + "']").should("exist");
            }
        });
    });
    it('Check navigator battery', () => {
        cy.window().then(w => {
            cy.wrap(w.navigator.getBattery()).then(function (battery) {

                cy.get("#navigator-battery-charging").should('have.text', battery.charging.toString());
                cy.get("#navigator-battery-chargingTime").should('have.text', battery.chargingTime.toString());
                cy.get("#navigator-battery-dischargingTime").should('have.text', battery.dischargingTime.toString());
                cy.get("#navigator-battery-level").should('have.text', battery.level.toString());
            });
        });
    });
    it('Check navigator position', () => {
        cy.window().then(w => {
            const coordinates = {
                timestamp: 1606859690000,
                coords: {
                    latitude: 43.5,
                    longitude: 13.2,
                    altitude: 150.6,
                    accuracy: 1.9,
                    altitudeAccuracy: 2.6,
                    heading: 90.9,
                    speed: 100.7
                }
            };
            cy.stub(w.navigator.geolocation, "getCurrentPosition", (cb, err, opt) => {
                expect(opt).to.deep.equal({ maximumAge: 3600000, timeout: 60000, enableHighAccuracy: true });
                return cb(coordinates);
            });
            var watchCallBack;
            cy.stub(w.navigator.geolocation, "watchPosition", (cb, err, opt) => {
                watchCallBack = cb;
                return 90;
            });

            cy.get("#navigator-geolocation-get").click().then(() => {
                cy.get("#navigator-geolocation-timestamp").should('not.be.empty');
                cy.get("#navigator-geolocation-coords-latitude").should('have.text', '43.5');
                cy.get("#navigator-geolocation-coords-longitude").should('have.text', '13.2');
                cy.get("#navigator-geolocation-coords-altitude").should('have.text', '150.6');
                cy.get("#navigator-geolocation-coords-accuracy").should('have.text', '1.9');
                cy.get("#navigator-geolocation-coords-altitudeAccuracy").should('have.text', '2.6');
                cy.get("#navigator-geolocation-coords-heading").should('have.text', '90.9');
                cy.get("#navigator-geolocation-coords-speed").should('have.text', '100.7');
                watchCallBack(coordinates);
                cy.get("#navigator-geolocation-changed").should('have.text', '1')
                    .then(() => {
                        cy.spyFix(w.navigator.geolocation, "clearWatch", w);
                        cy.get("#navigator-geolocation-event-change-stop").click().then(() => {
                            expect(w.navigator.geolocation.clearWatch).to.be.calledWith(90);
                        })
                    });
            });

        });
    });
    it('Check storage', function () {
        cy.window().then(w => {
            cy.get("#navigator-storage-getStorageEstimate").click()
                .then(() => {
                    if (w.navigator.storage) {
                        cy.wrap(w.navigator.storage.estimate()).then(e => {
                            cy.get("#navigator-storage-estimate-quota").invoke('text').should(v => {
                                expect(parseInt(v, 10)).to.be.below(e.quota * 1.1);
                                expect(parseInt(v, 10)).to.be.above(e.quota * 0.9);
                            });
                            cy.get("#navigator-storage-estimate-usage").should('have.text', e.usage.toString());

                        });
                        cy.wrap(w.navigator.storage.persist()).then((p) => {
                            cy.get("#navigator-storage-storagePersist").should('have.text', p.toString());
                        });
                        cy.wrap(w.navigator.storage.persisted()).then((p) => {
                            cy.get("#navigator-storage-storagePersisted").should('have.text', p.toString());
                        });
                    }
                });
        });
    });
    it('Check methods', function () {
        cy.window().then(w => {
            if (!w.navigator.canShare) {
                w.navigator.canShare = function (data) { return true; };
            }
            cy.spyFix(w.navigator, 'canShare', w);
            cy.get("#navigator-canShare-button").click().then(() => {
                expect(w.navigator.canShare).to.be.calledOnce;
                cy.get("#navigator-canShare").should('have.text', 'true');
            });


            if (!w.navigator.share) {
                w.navigator.share = function (data) { return true; }
            }
            cy.spyFix(w.navigator, 'share', w);
            cy.get("#navigator-share").click().then(() => {
                expect(w.navigator.share).to.be.calledOnce;
            });
            if (w.navigator.registerProtocolHandler) {
                cy.spyFix(w.navigator, 'registerProtocolHandler', w);
                cy.get("#navigator-registerProtocolHandler").click().then(() => {
                    expect(w.navigator.registerProtocolHandler).to.be.calledOnce;
                });
            }

            if (w.navigator.sendBeacon) {
                cy.spyFix(w.navigator, 'sendBeacon', w);
                cy.get("#navigator-sendBeacon").click().then(() => {
                    expect(w.navigator.sendBeacon).to.be.to.be.calledWith("/test", "BLBLA");
                });
            }

            cy.spyFix(w.navigator, 'vibrate', w);
            cy.get("#navigator-vibrate").click().then(() => {
                expect(w.navigator.vibrate).to.be.to.be.calledWith([100, 50, 1000]);
            });
        });

    });
})
