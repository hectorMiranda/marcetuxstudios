# Apple IAP server-side receipt validation.
# Validates against production first; falls back to sandbox on status 21007.
require 'net/http'
require 'json'
require 'uri'

class ReceiptValidator
  PROD_URI    = URI('https://buy.itunes.apple.com/verifyReceipt')
  SANDBOX_URI = URI('https://sandbox.itunes.apple.com/verifyReceipt')

  def self.validate(receipt_data, product_id:)
    new(receipt_data, product_id: product_id).validate
  end

  def initialize(receipt_data, product_id:)
    @receipt_data = receipt_data
    @product_id   = product_id
  end

  def validate
    response = verify(PROD_URI)

    # 21007 = receipt is a sandbox receipt; retry against sandbox endpoint
    if response['status'] == 21007
      response = verify(SANDBOX_URI)
    end

    return { valid: false, error: "status #{response['status']}" } unless response['status'] == 0

    in_app = Array(response.dig('receipt', 'in_app'))
    match  = in_app.find { |item| item['product_id'] == @product_id }
    { valid: !match.nil?, transaction_id: match&.fetch('transaction_id') }
  end

  private

  def verify(uri)
    body = JSON.generate({ 'receipt-data' => @receipt_data })
    http = Net::HTTP.new(uri.host, uri.port)
    http.use_ssl = true
    JSON.parse(http.post(uri.path, body, 'Content-Type' => 'application/json').body)
  end
end
