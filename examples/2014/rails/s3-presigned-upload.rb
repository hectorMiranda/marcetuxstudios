# Generate a presigned POST URL for direct browser-to-S3 upload.
# The browser POSTs the file directly; the app server is not in the data path.
# Requires the 'aws-sdk' gem (v1.x API).

require 'aws-sdk'

class UploadsController < ApplicationController
  def presign
    s3     = AWS::S3.new(region: 'us-east-1')
    bucket = s3.buckets[ENV['S3_BUCKET']]
    key    = "uploads/photos/#{current_user.id}/#{SecureRandom.uuid}.jpg"

    # Presigned form expires in 5 minutes; S3 rejects uploads after that.
    form = bucket.presigned_post(
      key:            key,
      content_type:   'image/jpeg',
      acl:            'private',
      expires:        Time.now + 300,
      content_length_range: (1..(5 * 1024 * 1024))  # 1 byte to 5 MB
    )

    render json: { url: form.url.to_s, fields: form.fields, key: key }
  end

  def confirm
    # Called by the browser after the S3 upload completes.
    current_user.update!(photo_key: params[:key])
    head :ok
  end
end
