using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleMobileAdsObject : MonoBehaviour
{
	private BannerView bannerView;
	private InterstitialAd interstitial;
	private RewardedAd rewardedAd;

	public void Start()
	{
		// https://developers.google.com/admob/unity/banner?hl=ko
		string appId = "ca-app-pub-3940256099942544~3347511713";

		MobileAds.Initialize(appId);
		RequestBanner();
		RequestInterstitial();
		RequestRewardedAd();
	}

	private void RequestBanner()
	{
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		// Create a 320x50 banner at the top of the screen.
		this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		this.bannerView.LoadAd(request);

		bannerView.Show();
	}

	public void RequestInterstitial()
	{
		string adUnitId = "ca-app-pub-3940256099942544/1033173712";
		this.interstitial = new InterstitialAd(adUnitId);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);
	}

	public void RequestRewardedAd()
	{
		string adUnitId = "ca-app-pub-3940256099942544/5224354917";
		this.rewardedAd = new RewardedAd(adUnitId);

		AdRequest request = new AdRequest.Builder().Build();

		// Load the rewarded ad with the request.
		this.rewardedAd.LoadAd(request);
	}

	public void ShowInterstitial()
	{
		interstitial.Show();
		RequestInterstitial();
	}

	private void ShowRewardedAd()
	{
		rewardedAd.Show();
		RequestRewardedAd();
	}

	internal void ShowRewardedAdForMeowney()
	{
		rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
		ShowRewardedAd();
	}

	public void HandleUserEarnedReward(object sender, EventArgs args)
	{
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetRewardMeowney();
	}

}