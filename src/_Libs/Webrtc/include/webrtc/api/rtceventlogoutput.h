/*
 *  Copyright (c) 2017 The WebRTC project authors. All Rights Reserved.
 *
 *  Use of this source code is governed by a BSD-style license
 *  that can be found in the LICENSE file in the root of the source
 *  tree. An additional intellectual property rights grant can be found
 *  in the file PATENTS.  All contributing project authors may
 *  be found in the AUTHORS file in the root of the source tree.
 */

#ifndef API_RTCEVENTLOGOUTPUT_H_
#define API_RTCEVENTLOGOUTPUT_H_

#include <string>

namespace webrtc {

// NOTE: This class is still under development and may change without notice.
class RtcEventLogOutput {
 public:
  virtual ~RtcEventLogOutput() = default;

  // An output normally starts out active, though that might not always be
  // the case (e.g. failed to open a file for writing).
  // Once an output has become inactive (e.g. maximum file size reached), it can
  // never become active again.
  virtual bool IsActive() const = 0;

  // Write encoded events to an output. Returns true if the output was
  // successfully written in its entirety. Otherwise, no guarantee is given
  // about how much data was written, if any. The output sink becomes inactive
  // after the first time |false| is returned. Write() may not be called on
  // an inactive output sink.
  virtual bool Write(const std::string& output) = 0;
};

}  // namespace webrtc

#endif  // API_RTCEVENTLOGOUTPUT_H_
