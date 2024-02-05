const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target: "https://api:7281",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
