mergeInto(LibraryManager.library, {


  CheckSdkInit: function () {

    // === Unity недоступен ===
    if (!unity) return;

    // === Обработка ===
    if (!sdk) unity.SendMessage('YandexGames', 'HTML_OnSdkInitChecked', 0);
    else unity.SendMessage('YandexGames', 'HTML_OnSdkInitChecked', 1);
    
  },

  CheckPaymentsAvailable: function () {

    // === Unity недоступен ===
    if (!unity) return;

    // === Обработка ===
    if (!payments) unity.SendMessage('YandexGames', 'HTML_OnPaymentsAvailableChecked', 0);
    else unity.SendMessage('YandexGames', 'HTML_OnPaymentsAvailableChecked', 1);
    
  },

  CheckPlayerInit: function () {

    // === Unity недоступен ===
    if (!unity) return;

    // === Обработка ===
    if (!player) unity.SendMessage('YandexGames', 'HTML_OnPlayerInitChecked', 0);
    else unity.SendMessage('YandexGames', 'HTML_OnPlayerInitChecked', 1);
    
  },


  RequestPurchasing: function(productId) {
    
    // === SDK недоступен ===
    if (!payments) {
      unity.SendMessage('YandexGames', 'HTML_OnPurchaseHandled', 0); 
      console.log("Yandex payments not available!");
      return;
    }

    // === Обработка ===
    payments.purchase({ id: UTF8ToString(productId) }).then(purchase => {

      // Покупка успешно совершена!
      window.focus();
      unity.SendMessage('YandexGames', 'HTML_OnPurchaseHandled', 1); 

    }).catch(err => {

      // Покупка не удалась: в консоли разработчика не добавлен товар с таким id,
      // пользователь не авторизовался, передумал и закрыл окно оплаты,
      // истекло отведенное на покупку время, не хватило денег и т. д.

      window.focus();
      unity.SendMessage('YandexGames', 'HTML_OnPurchaseHandled', 0);
      console.log(err);

    });
    
  },


  ShowRewarded: function () {
    
    // === SDK недоступен ===
    if (!sdk.adv.showRewardedVideo) {
      unity.SendMessage('YandexGames', 'HTML_OnRewardedAction', 'Closed');
      console.log("Yandex ads not available!");
      return;
    }

    // === Обработка ===
    sdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          unity.SendMessage('YandexGames', 'HTML_OnRewardedAction', 'Opened'); 
          console.log('Video ad open.');
        },

        onRewarded: () => {
          unity.SendMessage('YandexGames', 'HTML_OnRewardedAction', 'Rewarded');
          console.log('Rewarded!');
        },

        onClose: () => {
          unity.SendMessage('YandexGames', 'HTML_OnRewardedAction', 'Closed');
          console.log('Video ad closed.');
        }, 

        onError: (e) => {
          unity.SendMessage('YandexGames', 'HTML_OnRewardedAction', 'Failed');
          console.log('Error while open video ad:', e);
        }
      }
    });

  },


  ShowInterstitial: function () {
    
    // === SDK недоступен ===
    if (!sdk.adv.showFullscreenAdv) {
      unity.SendMessage('YandexGames', 'HTML_OnInterstitialAction', 'Closed');
      console.log("Yandex ads not available!");
      return;
    }

    // === Обработка ===
    sdk.adv.showFullscreenAdv({
      callbacks: {
        onOpen: () => {
          unity.SendMessage('YandexGames', 'HTML_OnInterstitialAction', 'Opened');
          console.log("Interstitial opened");
        },

        onClose: function(wasShown) {
          unity.SendMessage('YandexGames', 'HTML_OnInterstitialAction', 'Closed');
          console.log("Interstitial closed");
        },

        onError: function() {
          unity.SendMessage('YandexGames', 'HTML_OnInterstitialAction', 'Failed');
          console.log("Interstitial error");
        },
      }
    });
    
    
  },

  SendSaves: function (savesData) {
    
    // === SDK недоступен ===
    if (!player.setData) {
      unity.SendMessage('YandexGames', 'HTML_OnSavesSent', 0);
      console.log("Yandex player not available!");
      return;
    }

    // === Обработка ===
    player.setData({
      data: UTF8ToString(savesData),
    }).then(() => {
      unity.SendMessage('YandexGames', 'HTML_OnSavesSent', 1);
      console.log('Player saves sent');
    });
    
    
  },

  RequestSaves: function () {
    
    console.log('Request saves');

    player.getData(["data"]).then((data) => {
      
      console.log('Saves received');
      
      if (data.data) 
      {
        unity.SendMessage('YandexGames', 'HTML_OnSavesReceived', data.data.toString());
        console.log('Player saves received');
      }
      else 
      {
        unity.SendMessage('YandexGames', 'HTML_OnSavesReceived', '');
        console.log('Player saves received (empty data)');
      }

      
    });
    

  },


  RequestReview: function () {

    // === SDK недоступен ===
    if (!sdk.feedback.canReview) {
      console.log("Yandex review not available!");
      return;
    }
    
    // === Обработка ===
    sdk.feedback.canReview()
      .then(({ value, reason }) => {
        if (value) {
          unity.SendMessage('YandexGames', 'HTML_OnReviewOpened');
          sdk.feedback.requestReview()
            .then(({ feedbackSent }) => {
              console.log(feedbackSent);
              unity.SendMessage('YandexGames', 'HTML_OnReviewClosed');
            })
        } else {
          console.log(reason)
        }
    });
    

  },

  RequestPurchaseConsuming: function (productId) {

    var consumableProductId = UTF8ToString(productId);

    console.log('Consuming product ' + consumableProductId)
    payments.getPurchases().then(purchases => purchases.forEach(consumePurchase));

    function consumePurchase(purchase) {
      console.log('Checking product for consuming: Yandex ID: ' + purchase.productID + '; App ID: ' + consumableProductId);
      console.log(purchase.productID === consumableProductId);

      if (purchase.productID.toString() === consumableProductId) {

        payments.consumePurchase(purchase.purchaseToken);
        console.log('Product consumed: ' + purchase.purchaseToken);
      }
        
    }
    

  },
  

  RequestPurchasesIds: function () {

    payments.getPurchases().then(purchases => {
      var purchasedProductIds = ''; 

      purchases.forEach((purchase) => {
        purchasedProductIds = purchasedProductIds + purchase.productID + ',';
      });

      unity.SendMessage('YandexGames', 'HTML_OnPurchasesReceived', purchasedProductIds);
    });

  },


  ShowBanner: function () {

    ysdk.adv.getBannerAdvStatus().then(({ stickyAdvIsShowing , reason }) => {
      if (stickyAdvIsShowing) {

        // Баннер сейчас показывается

      } else if (reason) {

        // Ошибка показа баннера
        // ADV_IS_NOT_CONNECTED — не подключены баннеры;
        // UNKNOWN — ошибка показа рекламы на стороне Яндекса

        console.log(reason)
      } else {

        // Баннер сейчас не показывается
        ysdk.adv.showBannerAdv()
      }
    });

  },

  HideBanner: function () {

    ysdk.adv.getBannerAdvStatus().then(({ stickyAdvIsShowing , reason }) => {
      if (stickyAdvIsShowing) {

        // Баннер сейчас показывается
        ysdk.adv.hideBannerAdv()

      } else if (reason) {

        // Ошибка показа баннера
        // ADV_IS_NOT_CONNECTED — не подключены баннеры;
        // UNKNOWN — ошибка показа рекламы на стороне Яндекса

        console.log(reason)
      } else {

        // Баннер сейчас не показывается
        
      }
    });

  },

  GetCatalogPrices: function () {

    payments.getCatalog().then(products => {

      var pricesData = '';
      products.forEach((product) => {
        pricesData = pricesData + product.id + ',' + product.price + ';';
      });

      unity.SendMessage('YandexGames', 'HTML_OnPricesReceived', pricesData);
    });


  },

  InitializePlayer: function () {
    InitPlayer();
  },

  InitializePayments: function () {
    InitPayments();
  },


  RequestLanguage: function () {
    unity.SendMessage('YandexGames', 'HTML_OnLanguageReceived', sdk.environment.i18n.lang);
  },


  GameReady: function () {
    sdk.features.LoadingAPI.ready();
    console.log('Game ready');
  },


  _SendEvent: function(yandexMetricaCounterId, eventNameUtf8, eventDataUtf8) {
    const eventName = UTF8ToString(eventNameUtf8);
    const eventData = UTF8ToString(eventDataUtf8);
    try {
      const eventDataJson = eventData === '' ? undefined : JSON.parse(eventData);
      ym(yandexMetricaCounterId, 'reachGoal', eventName, eventDataJson);
      console.log('jslib: Event sent');

    } catch (e) {
      console.error('Yandex Metrica send event error: ', e.message);
    }

  },




});